/*
 * 由SharpDevelop创建。
 * 用户： Yue
 * 日期: 2017/4/10
 * 时间: 15:25
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;

namespace DocDiy
{
	/// <summary>
	/// Description of ModeUtil.
	/// </summary>
	public class ModeModel
	{
		public int httpCount{set;get;}
		public int fileCount{set;get;}
		public int otherCount{set;get;}
		public List<string> httpList{set;get;}
		public List<string> fileList{set;get;}
		public List<string> otherList{set;get;}
		public ModeModel()
		{
		}
		public bool isHttpMode(List<string> contentList){
			httpList = new List<string>();
			fileList = new List<string>();
			otherList = new List<string>();
			httpCount = 0;
			otherCount = 0;
			fileCount = 0;
			
			foreach(string item in contentList){
				string it = item.Trim();
				if(item.StartsWith("http:")){
					httpCount++;
					httpList.Add(it);
				}else if(item.StartsWith("file:")){
					fileCount++;
					fileList.Add(it);
				}
				else{
					otherCount++;
					otherList.Add(it);
				}
			}
			return httpCount>fileCount;
		}
	}
}
