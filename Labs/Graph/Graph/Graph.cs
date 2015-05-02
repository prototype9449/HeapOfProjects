using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Graph 
    {
        
        private Dictionary<int, HashSet<int>> _setVertex;  
      
        /// <param name="setVertex"> Set vertex with their edges </param>
        public Graph(Dictionary<int, HashSet<int>> setVertex) 
        { 
            this._setVertex = setVertex;           
        }  
      
        /// <param name="vertex"> isolated vertex </param>
        public Graph(int vertex)
        {
            this._setVertex = new Dictionary<int, HashSet<int>>();
            this._setVertex.Add(vertex, new HashSet<int>());
        }

        public Graph()
        {
            this._setVertex = new Dictionary<int, HashSet<int>>();
        }

        /// <summary>
        /// Add vertex to the graph
        /// </summary>
        /// <param name="vertex"> Number vertex </param>
        /// <param name="edges"> edges this vertex </param>        
        public void AddVertex(int vertex, HashSet<int> edges)
        {
            if (!_setVertex.ContainsKey(vertex))
            {
                _setVertex.Add(vertex, edges);
                foreach (var temp in edges)
                {
                    _setVertex[temp].Add(vertex);
                }
            }
            else
            {
                throw new Exception("Adding of the already existing vertices");
            }
        
        }

        /// <summary>
        /// Remove vertex of the graph
        /// </summary>
        /// <param name="vertex"> Number of vertex </param>        
        public void RemoveVertex(int vertex)
        {
            if (_setVertex.ContainsKey(vertex))
            {
                foreach (var temp in _setVertex[vertex])
                {
                    if (_setVertex[temp].Contains(vertex))
                    {
                        _setVertex[temp].Remove(vertex);
                    }
                }
                _setVertex.Remove(vertex);
            }
            else
            {
                throw new Exception("Remove of not existing vertex");
            }
        }

        public int Count
        {
            get { return _setVertex.Count(); }
        }

        public Dictionary<int, HashSet<int>> SetVertex
        {
            get { return _setVertex; }
        }
    }
}
