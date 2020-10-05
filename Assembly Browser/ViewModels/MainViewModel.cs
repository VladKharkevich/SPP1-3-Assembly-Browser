using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AssemblyInformation;

namespace Assembly_Browser.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
		private string fAssemblyName;

        public event PropertyChangedEventHandler PropertyChanged;
		private DelegateCommand fOpenFileCommand;
		
		private AssemblyResult fAssemblyResult;
		private Models.MainModel fBrowserModel;	

		public string lAssemblyName
        {
            get
            {
				return "Assembly: " + fAssemblyName;
			}
			set
            {
				fAssemblyName = value;
				OnPropertyChanged();
			}
        }
		 
		public AssemblyResult Result 
		{
			get
			{
				return fAssemblyResult;
			}
			set
			{
				fAssemblyResult = value;
				OnPropertyChanged();
			}
		}

		public void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		public DelegateCommand OpenFileCommand
		{
			get
			{
				return fOpenFileCommand ?? (fOpenFileCommand = new DelegateCommand(OpenFileMethod));
			}
		}

		public void OpenFileMethod(object obj)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "Dll files (*.dll)|*.dll";

				if (openFileDialog.ShowDialog() == true)
				{
					if (fBrowserModel == null)
						fBrowserModel = new Models.MainModel();
					Result = fBrowserModel.GetResult(openFileDialog.FileName);
				}
				lAssemblyName = Result.AssemblyName;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
		/*private void btnOpenFile_Click(object sender)
		{
			lAssemblyName.Foreground = Brushes.Black;
            OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Dll files (*.dll)|*.dll";
			if (openFileDialog.ShowDialog() == true)
			{
				tTreeAssembly.Items.Clear();
				try
				{
					if (fBrowserModel == null)
						fBrowserModel = new Models.MainModel();
					fAssemblyResult = fBrowserModel.GetResult(openFileDialog.FileName);
					lAssemblyName.Content = "Assembly: " + assemblyResult.AssemblyName;
					foreach (AssemblyNamespace assemblyNamespace in assemblyResult.AssemblyNamespaces)
					{
						TreeViewItem namespaceChild = new TreeViewItem();
						namespaceChild.Header = "Namespace: " + assemblyNamespace.Namespace;
						foreach (AssemblyType assemblyType in assemblyNamespace.AssTypes)
						{
							TreeViewItem typeChild = new TreeViewItem();
							typeChild.Header = "Type: " + assemblyType.AssType;
							foreach (String methodName in assemblyType.AssMethods)
							{
								TreeViewItem methodChild = new TreeViewItem();
								methodChild.Header = "Method: " + methodName;
								typeChild.Items.Add(methodChild);
							}
							foreach (String fieldName in assemblyType.AssFields)
							{
								TreeViewItem fieldChild = new TreeViewItem();
								fieldChild.Header = "Field: " + fieldName;
								typeChild.Items.Add(fieldChild);
							}
							foreach (String propertyName in assemblyType.AssProperties)
							{
								TreeViewItem propertyChild = new TreeViewItem();
								propertyChild.Header = "Property: " + propertyName;
								typeChild.Items.Add(propertyChild);
							}
							namespaceChild.Items.Add(typeChild);
						}
						tTreeAssembly.Items.Add(namespaceChild);
					}
				}
				catch (System.BadImageFormatException)
				{
					lAssemblyName.Content = "It's not .NET assembly!!!";
					lAssemblyName.Foreground = Brushes.Red;
				}
			}
		}*/
	}
}
