using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace WpfApp1.Repository
{
    public class operations : Ioperations
    {
        //tHIS IS THE LOCATION OF THE key-value pair text file
        string sfileLocation = @"D:\Input1.txt";
       
        public Hashtable Getkeyvaluedata()
        {
              Hashtable keyPairs1 = new Hashtable();
           //  IniParser parser = new IniParser(sfileLocation);
             TextReader iniFile = null;
            //  IniParser parser = new IniParser(@"D:\Input1.txt");
            String strLine = null;
        ;
            if (File.Exists(sfileLocation))
            {
                try
                {
                    iniFile = new StreamReader(sfileLocation);

                    strLine = iniFile.ReadLine();

                    while (strLine != null)
                    {
                        strLine = strLine.Trim().ToUpper();

                        if (strLine != "")
                        {
                            // SectionPair sectionPair;
                            KeyvaluesEntitiy keyvaluesEntitiy = new KeyvaluesEntitiy();
                            var lineWithoutComments = strLine.Split(new[] { "//" }, StringSplitOptions.None).FirstOrDefault();
                            if (String.IsNullOrWhiteSpace(lineWithoutComments)) continue;
                            //split key and value
                            var split = lineWithoutComments.Split(new[] { "," }, StringSplitOptions.None);
                            if (split.Length < 2) continue;
                            keyvaluesEntitiy.Key = split[0].Trim();
                            keyvaluesEntitiy.Value = split[1].Trim();
                            //parser.keyPairs11.Add(keyvaluesEntitiy.Key, keyvaluesEntitiy.Value);
                            keyPairs1.Add(keyvaluesEntitiy.Key, keyvaluesEntitiy.Value);
                        }

                        strLine = iniFile.ReadLine();
                    }

                    iniFile.Close();


                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (iniFile != null)
                        iniFile.Close();
                }
            }
            else
            {

                //throw new FileNotFoundException("Unable to locate " + iniPath);
                using (StreamWriter sw = File.CreateText(sfileLocation))
                {
                    sw.WriteLine("HEALTH::integer::Employee_No,501834");
                    sw.WriteLine("HEALTH::string::Name,Priyanka Singh");
                    sw.WriteLine("HEALTH::Boolean::Active,1");
                    sw.WriteLine("SAFETY::integer::Employee_No,82864");
                    sw.WriteLine("SAFETY::string::Name,Vivek Singh");
                    sw.WriteLine("SAFETY::Boolean::Active,1");
                    sw.WriteLine("HEALTH::string::DEPT,IS00");
                    sw.WriteLine("HEALTH::integer::MOBILE_NO,7838451542");
                }

            }
            return keyPairs1 ;
          
        }

        public List<dynamic> KeyPairDetails()
        {
          //  IniParser parser = new IniParser(sfileLocation);
            var result = new List<dynamic>();
            foreach (DictionaryEntry de in Getkeyvaluedata())
            {
                result.Add(new
                {
                    Key = de.Key,
                    Value = de.Value
                });
            }
            return result.ToList();
          
        }

        public void Save(string keyuserinput,string valueuserinput)
        {
            KeyvaluesEntitiy keyvaluesEntitiy = new KeyvaluesEntitiy();
            TextReader iniFile = null;
            iniFile = new StreamReader(sfileLocation);
            String strLine = null;
            strLine = iniFile.ReadLine();
            int cnt = 0;
         
            while (strLine != null)
            {
                strLine = strLine.Trim().ToUpper();

                if (strLine != "")
                {
                    var split = strLine.Split(new[] { "," }, StringSplitOptions.None);
                    if (split.Length < 2) continue;
                    keyvaluesEntitiy.Key = split[0].Trim();
                    keyvaluesEntitiy.Value = split[1].Trim();



                    if (keyvaluesEntitiy.Key.ToUpper().Contains(keyuserinput.ToUpper()))
                    {
                        cnt = 1;
                        break;
                    }
                    else
                    {
                        cnt = 0;
                    }



                }
                strLine = iniFile.ReadLine();
               
            }
            iniFile.Close();
            if (cnt == 0)
            {
                var word = keyuserinput.ToUpper() + "," + valueuserinput.ToUpper(); 
                
                iniFile.Close();
                using (StreamWriter sw = File.AppendText(sfileLocation))
                {
                    iniFile.Dispose();
                    sw.WriteLine(word);
                  

                }
                MessageBox.Show("Key-Value pair successfully Added");
                KeyPairDetails();
            }
            else
            {
                MessageBox.Show("Key-Value pair is already Exist!");
            }
        }

        public void update(string keyuserinput, string valueuserinput)
        {
            KeyvaluesEntitiy keyvaluesEntitiy = new KeyvaluesEntitiy();
            TextReader iniFile = null;
            iniFile = new StreamReader(sfileLocation);
            String strLine = null;
            strLine = iniFile.ReadLine();
            int cnt = 0;
            int _linenumber = 0;
            while (strLine != null)
            {
                strLine = strLine.Trim().ToUpper();
                _linenumber++;
                if (strLine != "")
                {
                    var split = strLine.Split(new[] { "," }, StringSplitOptions.None);
                    if (split.Length < 2) continue;
                    keyvaluesEntitiy.Key = split[0].Trim();
                    keyvaluesEntitiy.Value = split[1].Trim();

                    if (keyvaluesEntitiy.Key.ToUpper().Contains(keyuserinput.ToUpper()))
                    {
                        cnt = 1;
                        break;
                    }
                    else
                    {
                        cnt = 0;
                    }



                }
                strLine = iniFile.ReadLine();
            }
            if (cnt == 1)
            {
                var word = keyuserinput.ToUpper() + "," + valueuserinput.ToUpper(); // delete the selected line

                iniFile.Close();

                string[] arrLine = File.ReadAllLines(sfileLocation);
                arrLine[_linenumber - 1] = word;
                iniFile.Close();
                File.WriteAllLines(sfileLocation, arrLine);



                MessageBox.Show("Key-Value pair successfully Updated");
                KeyPairDetails();
            }
            else
            {
                MessageBox.Show("Key-Value pair record doesn't Exist!");
            }
        }
    }
}
