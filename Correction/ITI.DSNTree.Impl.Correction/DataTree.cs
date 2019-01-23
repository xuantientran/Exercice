using ITI.DSNTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class DataTree : IDataTree
	{
		IDataBlock _root;
		IDsnTree _dsnTree;
		int _count = 0;

		public DataTree(IDsnTree dsnTree)
		{
			_dsnTree = dsnTree;
		}

		public IDataBlock Root { get => _root; set => _root = value; }

		public int Count => _count;

		public IDsnTree DsnTree => _dsnTree;

		public void LoadDataTree(string path)
		{

			int lineCouter = 0;

			Stopwatch muniteur = new Stopwatch();
			muniteur.Start();
			//, Encoding.GetEncoding("iso-8859-1")
			using (StreamReader reader = new StreamReader(path))
			{
				//current data bloc
				IDataBlock block = null;

				//last block can be consider as parent block in some condition
				IDataBlock parentBlock = null;

				//Current node for helping to structure the block
				IDsnNode node = null;

				Dictionary<string, IDataBlock> lastParentBlockDictionary = new Dictionary<string, IDataBlock>();

				//For stocking a temporary dictionary
				Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

				//an artificial root has to be definited
				_root = new DataBlock(_dsnTree.Root.Id);
				lastParentBlockDictionary.Add(_root.Id, _root);
				_count++;

				//data leaf
				DataLeaf leaf = null;

				string blockKey = "";
				string lastBlockKey = "";
				string key = "";
				string val = "";
				string line;
				string[] kv;

				//for detecting the change
				bool keyRepeat;

				//for detection the block change 
				bool blockChange;

				while ((line = reader.ReadLine()) != null)
				{
					lineCouter++;
					kv = line.Split(',');
					if (kv.Length < 2)
						continue;

					blockKey = kv[0].Substring(0, 10);
					key = kv[0];
					kv[0] = "";
					//S10.G01.00.002,'DSTI'
					val = string.Join(",", kv);
					val = val.Substring(2, val.Length - 3);

					//répétition de rubriques arrive lors que la même rubrique se trouve dans le même bloc
					if (dataDictionary.ContainsKey(key))
						keyRepeat = true;
					else
						keyRepeat = false;

					if (string.Compare(blockKey, lastBlockKey) == 0)
						blockChange = false;
					else
						blockChange = true;

					//début du corps
					if (blockChange)
					{
						//On récupère les information sur le bloc courant
						//pour créer un nouveau
						((DsnTree)_dsnTree).DsnNodes.TryGetValue(blockKey, out node);
						block = new DataBlock(blockKey);
						_count++;
						//On récupère le bloc parent pour créer les liens parentaux
						lastParentBlockDictionary.TryGetValue(node.Parent.Id, out parentBlock);

						//le bloc courant reconnait son parent
						block.Parent = parentBlock;

						//le parent reconnait son enfant
						parentBlock.Children.Add(block);

						//Si le bloc courant a des enfants
						//alors il doit entrer dans le dictionaire des parents identifié par son Id en écrasant un autre parent qui a le même id que lui
						if (node.Children.Count > 0)
							lastParentBlockDictionary[blockKey] = block;

						//On crée le premier dictionaire de rubriques pour le nouveau bloc
						//et l'joute dans la liste du bloc
						//le bloc suivant est créé dans le cas de la répétition d'une rubrique
						dataDictionary = new Dictionary<string, string>();
						leaf = new DataLeaf(block, dataDictionary);
						block.Leaves.Add(leaf);

					}
					else
					//le cas du même bloc
					{
						if (keyRepeat)
						//Il faut créer et ajouter un dictionaire des rubriques.
						//C'est le cas où un bloc a des répétions de rubriques.
						{
							dataDictionary = new Dictionary<string, string>();
							leaf = new DataLeaf(block, dataDictionary);
							block.Leaves.Add(leaf);
						}
						else
						{
						}
					}
					//Le bon bloc et le bon dictionaire des rubriques ont été crées auparavant
					//On se contente d'ajouter le paire de clé, valeur pour chaque ligne
					dataDictionary.Add(key, val);
					//if (cles[1] == 210030)
					//	Console.WriteLine(cleRubrique + ":" + valeur);
					//fin du corps
					lastBlockKey = blockKey;
				}//Fin while reader.ReadLine()

			}//Fin using
			muniteur.Stop();
			Console.WriteLine("Loading data tree of " + lineCouter + " lines in " + muniteur.Elapsed + " seconds");
		}

		public List<IDataBlock> FindBlock(string id, IDataBlock block = null)
		{
			/*
			S40.G05.00
			Loading 8955580 lines in 25,4 seconds
			blocks stacked/total : 2 173 100/2 192 166 in 1,3 seconds
			0 found

			blocks stacked/total : 79 391/2 192 166 in 0,06 seconds

			S30.G01.00
			Loading 8955580 lines in 22,5 seconds
			blocks stacked/total : 79 391/2 192 166 in 0,06 seconds
			77 327 found
			*/
			//Stopwatch muniteur = new Stopwatch();
			//muniteur.Start();
			//get the level of the block
			//int stackCounter = 1;
			IDsnNode node;
			((DsnTree)_dsnTree).DsnNodes.TryGetValue(id, out node);
			int levelLimit = node.Level;

			List<IDataBlock> blocks = new List<IDataBlock>();
			IDataBlock current;

			Stack<IDataBlock> stack = new Stack<IDataBlock>();
			if (block == null)
				current = _root;
			else
				current = block;

			stack.Push(current);

			while (stack.Count > 0)
			{
				current = stack.Pop();

				((DsnTree)_dsnTree).DsnNodes.TryGetValue(current.Id, out node);

				if (node.Level < levelLimit)
				//the blocs with different levels
				{
					//stackCounter += current.Children.Count;
					for (var i = current.Children.Count - 1; i >= 0; i--)
						stack.Push(current.Children[i]);
				}
				else if (node.Level == levelLimit)
				{
					if (string.Compare(current.Id, id) == 0)
						blocks.Add(current);
				}

			}
			//muniteur.Stop();
			//Console.WriteLine("blocks stacked/total : " + stackCounter + "/" + _count + " in " + muniteur.Elapsed + " seconds");

			return blocks;
		}

		public override string ToString() => _root.ToString();

	}
}