using System;
using System.IO;

namespace Program{

	public class FileData{

		public FileData(string name, long size, bool is_dir, Dictionary<string, FileData>? children, FileData? parent){
			this.name = name;
			this.size = size;
			this.is_dir = is_dir;

			this.children = children;
			this.parent = parent;
		}

		
		public FileData (String path, FileData? parent){
			FileAttributes attributes = File.GetAttributes(path);
			
			if ((attributes & FileAttributes.Directory) == FileAttributes.Directory){

				DirectoryInfo info = new DirectoryInfo(path);

				var files = info.GetFiles();
				var dirs = info.GetDirectories();

				this.name = info.Name;
				this.size = 0;
				this.is_dir = true;
				this.children = new Dictionary<string, FileData>(files.Length + dirs.Length, null);
				this.parent = parent;
				
				
				foreach (var f in files){
					FileData data = new FileData(f.Name, f.Length, false, null, parent);
					this.children.Add(data.name, data);
				}
				foreach (var d in dirs){
					FileData data = new FileData(d.FullName, this);
					this.children.Add(data.name, data);
				}

			}
			else {
				FileInfo info = new FileInfo(path);
				this.name = info.Name;
				this.size = info.Length;
				this.is_dir = false;
				this.children = null;
				this.parent = parent;
		 
			}		
			
		}
			
		internal string name;
		internal Dictionary<string, FileData>? children;
		internal FileData? parent;
		
		internal long size;
		internal bool is_dir;
						
	}

	public class FileTree{

		private FileData root;
	
		public FileTree(String path){
			FileAttributes rootAttr = File.GetAttributes(path);

			//Long way of saying "if path isn't a directory"
			if(!((rootAttr & FileAttributes.Directory) == FileAttributes.Directory)){
				throw new ArgumentException("Error: " + path + " is not a directory, and cannot be root file");
			}

			this.root = new FileData(path, null);
		}

		

		
	}
} 
