using System.IO;

namespace Program{

	public class FileData{

		public FileData(string name, uint size, bool is_dir){
			this.name = name;
			this.size = size;
			this.is_dir = is_dir;

			this.children = null;
			this.parent = null;
		}

			
		private string name {get;}
		private Dictionary<string, FileData> children;
		private FileData parent;
		
		private uint size;
		private bool is_dir;
						
	}

	public class FileTree{

		FileData root;
		uint size;

		public FileTree(String path){
			
		}

		
	}
} 
