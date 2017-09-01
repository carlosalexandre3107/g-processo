using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GProcessos.Dominio;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GProcessos.Serializado
{
    public class Arquivo
    {
        String caminhoFisico = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Serializado\\File.bin");
        public void SerializarProcessos(List<Processo> _processos)
        {
            FileStream fs = new FileStream(@caminhoFisico, FileMode.Create);
            using (fs)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, _processos);
                fs.Close();
            }
        }
        public List<Processo> DeserializarProcessos()
        {
            FileInfo fi = new FileInfo(@caminhoFisico);
            List<Processo> _processos = new List<Processo>();
            if (fi.Exists)
            {
                FileStream fs = new FileStream(@caminhoFisico, FileMode.Open);
                using (fs)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    _processos = (List<Processo>)bf.Deserialize(fs);
                    fs.Close();
                }
            }
            return _processos;
        }
    }
}
