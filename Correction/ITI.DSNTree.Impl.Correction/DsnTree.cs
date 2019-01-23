using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class DsnTree : IDsnTree
	{
		int _count;
		IDsnNode _root;
		Dictionary<string, IDsnNode> _dsnNodes;

		public DsnTree(string path)
		{
			_count = 0;
			_root = null;
			_dsnNodes = new Dictionary<string, IDsnNode>();
			LoadDsnTree(path);
		}

		void LoadDsnTree(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException("File does not exists");

			string exp = @"(.+)\s+\(([0-9]),([0-9]|\*)\)$";
			//On trouve toujours le parent au sommet de la pile
			Stack<IDsnNode> parentStack = new Stack<IDsnNode>();
			IDsnNode current;
			IDsnNode parent;
			//Il faut ajouter la racine artificielement
			_root = new DsnNode("S00.G00.00", null, "Root", 0, new DsnCardinality(1, 1));

			_dsnNodes.Add(_root.Id, _root);

			current = _root;
			parent = _root;

			//Toutes les rubriques se trouvent dans le fichiers envoi doivent augement un niveau
			int baseLevel = 1;
			int lastLevel = 0;
			string line;
			int nline = 0;
			int level = 0;
			int min;
			int max;
			//Traitement de la S10
			//, Encoding.GetEncoding("iso-8859-1")
			using (StreamReader lecture = new StreamReader(path))
			{
				while ((line = lecture.ReadLine()) != null)
				{
					nline++;
					//On ne traite pas les lignes vides
					if (string.IsNullOrEmpty(line))
						continue;

					string[] cv = line.Split('-');

					//On ne traite pas les lignes sans avoir le caratère '-'
					if (cv.Length == 1)
						continue;

					//On compte le nombre de tab pour calculer le niveau de la rubrique
					level = 0;
					for (int i = 0; i < cv[0].Length; i++)
					{
						if (cv[0][i] == '\t')
							level = i + 1;
						else
							break;
					}
					//Le niveau doit être absolu
					level += baseLevel;

					//On enlève tous les caratères \t pour trouver la rubrique
					string cle = cv[0].Trim();

					//On récupère le reste comme valeur
					cv[0] = "";
					string valeur = string.Join("-", cv).Substring(1).Trim();

					//la rubrique doit se trouver dans le dictionaire
					//alimanté des rubriques auparavant
					//if (dsn.NoeudBlocs.ContainsKey(cle))
					//	continue;

					int sautArriere = lastLevel - level;

					//si le niveau augmente par rapport au dernier niveau
					//Alors on met la dernier bloc dans la pile des parents
					//S10.G01.00 - Emetteur (1,1)
					//	S10.G01.01 - Contacts Emetteur(1, *)
					//sautArriere = -1
					if (sautArriere < 0)
					{
						parent = current;
						parentStack.Push(parent);
					}
					//Si le niveau déminue alors enlève autant de parents que néscessaire
					//pour trouver le bon parent.
					//			S70.G10.15 - Rémunérations (0,*)
					//	S80.G01.00 - Identification INSEE des établissements(1, *)
					//sautArriere = 2;
					else if (sautArriere > 0)
					{
						for (int i = 0; i < sautArriere; i++)
						{
							parent = parentStack.Pop();
						}
						parent = parent.Parent;
					}

					//On cherche les informations dans la ligne pour alimenter le noeu
					Match m = Regex.Match(valeur, exp);
					if (m.Success)
					{
						string label = m.Groups[1].Value;
						min = int.Parse(m.Groups[2].Value);
						if (string.Compare(m.Groups[3].Value, "*") == 0)
							max = 0;
						else
							max = int.Parse(m.Groups[3].Value);

						//on crée un noeu et spécise son parent
						current = new DsnNode(cle, parent, label, level, new DsnCardinality(min, max));
						//On ajoute le noeu dans le dictionaire
						_dsnNodes.Add(cle, current);
						_count++;
						lastLevel = level;
					}
				}
			}
		}

		public Dictionary<string, IDsnNode> DsnNodes => _dsnNodes;

		public int Count => _count;

		public IDsnNode Root => _root;

		public IDsnNode Find(string id)
		{
			_dsnNodes.TryGetValue(id, out IDsnNode node);
			return node;
		}

		public void Write(string path)
		{
			File.WriteAllText(path, ToString().Trim());
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			List<IDsnNode> nodes = new List<IDsnNode>();
			IDsnNode current = _root;
			IDsnNode node = _root;

			Stack<IDsnNode> stack = new Stack<IDsnNode>();

			stack.Push(current);

			while (stack.Count > 0)
			{
				current = stack.Pop();

				for (var i = current.Children.Count - 1; i >= 0; i--)
					stack.Push(current.Children[i]);

				if (current != _root)
					sb.AppendLine((new string('\t', current.Level -1)) + current.Id + " - " + current.Label + " (" + current.Cardinality.Min + "," + ((current.Cardinality.Max == 0) ? "*" : current.Cardinality.Max.ToString()) + ")");
			}

			return sb.ToString().Trim();
		}
	}
}