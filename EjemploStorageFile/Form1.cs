using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;

namespace EjemploStorageFile
{
    public partial class Form1: Form
    {
        public Form1 ()
        {
            InitializeComponent();
        }

        private async void botonleerfile_Click (object sender, EventArgs e)
        {
            //NECESITAMOS LA CLAVE DE ACCESO A STORAGE
            String key = CloudConfigurationManager.GetSetting("storagecnnstring");
            //NECESITAMOS ACCEDER A TODA LA CUENTA CLOUR STORAGE
            //PARA PODER ACCEDER A FILE
            CloudStorageAccount account = CloudStorageAccount.Parse(key);
            //PARA ACCEDER A CUALQUIER RECURSO, SE REALIZA MEDIANTE
            //UN CLIENT ESPECIALIZADO
            CloudFileClient client = account.CreateCloudFileClient();
            //DEBEMOS ACCEDER AL RECURSO (SHARED) COMPARTIDO
            //QUE HEMOS CREADO EN STORAGE FILE
            CloudFileShare fileshare = client.GetShareReference("ejemplo");
            //LOS FICHEROS LOS TENEMOS DIRECTAMENTE EN LA RAIZ DEL SHARED
            CloudFileDirectory raiz = fileshare.GetRootDirectoryReference();
            CloudFile file = raiz.GetFileReference("PENDIENTE.txt");
            String contenido = await file.DownloadTextAsync();
            this.txtcontenido.Text = contenido;
        }
    }
}
