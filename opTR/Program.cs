using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace opTR
{
    class Program
    {
        static String text;
        static String markdown;
        static String post; 

        static String[] specials = { "ş", "Ş", "ğ", "Ğ", "ü", "Ü", "İ", "ı", "ç", "Ç", "ö", "Ö" };
        static String[] numericals = { "&#351;", "&#350;", "&#287;", "&#286;", "&uuml;", "&Uuml;", "&#304;", "&#305;", "&ccedil;", "&Ccedil;", "&#246;", "&#214;" };
        static String[] files;

        static void Main(string[] args)
        {
            Console.WriteLine("Türkçe karakterleri ascii karşılıklarına çevir");

            try
            {
                String extension = args[0].Substring(args[0].IndexOf('.'));
                String path = args[0].Substring(0,args[0].LastIndexOf('\\'));
                files = Directory.GetFiles(path, "*"+extension);

                foreach(String file in files)
                {
                    try
                    {
                        TextReader tr = new StreamReader(file, Encoding.UTF8);
                        text = tr.ReadToEnd();
                        tr.Close();
                        markdown = text.Substring(0, text.IndexOf("---", 4));
                        post = text.Substring(text.LastIndexOf("---"));
                        replace();
                        text = markdown + post;
                        TextWriter tw = new StreamWriter(file, false, Encoding.UTF8);
                        tw.Write(text);
                        tw.Close();
                        Console.WriteLine(file.Substring(file.LastIndexOf('\\')+1));

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(file + " dosyasında bir hata oluştu.");
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Devam etmek için E tuşuna basınız.");
                        var a = Console.ReadKey();
                        if (a.KeyChar != 'E')
                        {
                            return;
                        }
                    }
                    finally
                    {
                    }
                }

            }catch(Exception e)
            {
                Console.WriteLine("Bir hata oluştu. Bir tuşa basınız.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }
            Console.WriteLine("İşlem tamamlandı. Bir tuşa basınız.");

            Console.ReadKey();
        }



        private static void replace()
        {
            for (int i = 0; i < specials.Length; i++)
            {
                markdown = markdown.Replace(specials[i], numericals[i]);
            }
        }
    }
}
