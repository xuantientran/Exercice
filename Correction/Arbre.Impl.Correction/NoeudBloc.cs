using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	public class NoeudBloc : INoeud
	{
		List<INoeud> _enfants;
		INoeud _parent = null;
		int _niveau = 0;
		readonly string _id;
		string _libelle;
		int _insMin = 0;
		int _insMax = 1;

		public NoeudBloc(string id)
		{
			_id = id;
			_enfants = new List<INoeud>();
		}

		public NoeudBloc(string id, INoeud parent)
		{
			_id = id;
			_parent = parent;
			_niveau = parent.Niveau + 1;
		_enfants = new List<INoeud>();
			parent.Enfants.Add(this);
		}

		public string Id => _id;

		public List<INoeud> Enfants { set => _enfants = value; get => _enfants; }

		public INoeud Parent { set => _parent = value; get => _parent; }

		public int Niveau { set => _niveau = value; get => _niveau; }

		public int InsMin { set => _insMin = value; get => _insMin; }

		public int InsMax { set => _insMax = value; get => _insMax; }

		public string Libelle { get => _libelle; set => _libelle = value; }

		public void AjouterEnfant(INoeud enfant)
		{
			enfant.Parent = this;
			enfant.Niveau = Niveau += 1;
			Enfants.Add(enfant);
		}

	}
}
