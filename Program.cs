using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Dynamic;
using static System.Net.Mime.MediaTypeNames;
using static System.Linq.Enumerable;
using System.Threading;

namespace Dormitory_console_0._0._2
{
    internal class Program
    {
        /// <summary>
        /// read data from a file and return a array , can be also use for reload
        /// read file -> big string -> separate Line -> separate comma -> create new arrayy and append data
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static string[,] ReLoad_Read_file(string path)                      //{{{{ ReadLoad_read_file }}}}
        {
            string[] line, sv2;
            //Create readstream
            StreamReader read_file = new StreamReader(path);
            string all_line = read_file.ReadToEnd().Trim();
            read_file.Close();
            //check data from file
            //Console.WriteLine(all_line); //checked
            //Create array sv  is all line of this big string 
            line = all_line.Split('\n');                                       //checked
            string[,] sv3 = new string[line.Length, 5];
            //Append data
            for (byte i = 0; i < line.Length; i++)
            {
                sv2 = line[i].Split(',');
                for (byte k = 0; k < 5; k++)
                {
                    sv3[i, k] = sv2[k];
                    //Console.WriteLine(sv3[i, k]);                         checked
                }
            }
            return sv3;
        }
        /// <summary>
        /// take people info and return a 2d array from them
        /// </summary>
        /// <returns></returns>
        static string[] name(string path)                               //preload  name col
        {
            string[,] sv2 = ReLoad_Read_file(path);
            string[] sv2_name = new string[sv2.Length / 5];
            for (int i = 0; i < sv2_name.Length; i++)
            {
                sv2_name[i] = sv2[i, 0];
            }
            return sv2_name;
        }
        static string[] ssn(string path)                                                   //preload ssn svs
        {
            string[,] sv2 = ReLoad_Read_file(path);
            string[] sv2_ssn = new string[sv2.Length / 5];
            for (int i = 0; i < sv2_ssn.Length; i++)
            {
                sv2_ssn[i] = sv2[i, 3];
            }
            return sv2_ssn;
        }
        static string[] code_room()                  //return room  support for check room
        {
            string path =
            "C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv.txt";
            string[,] sv2 = ReLoad_Read_file(path);
            string[] room9 = new string[sv2.Length / 5];
            //Console.WriteLine("lengh of sv2 " + sv2.Length/5);
            //show_2darray(sv2);
            for (int k = 0; k < room9.Length; k++)
            {
                room9[k] = sv2[k, 4];

            }
            //show_1d_array(room9);
            return room9;
        }
        /// <summary>
        /// Check room ,take room as parameter   and return string represent can be process by another function
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        static string Check_room(string room)                         //check room
        {
            string[] room9 = code_room();
            string[] room10 = "101 110 111 116 117 201 202 207 208 209 210 211 216 217 301 302 307 308 309 310 311 316 317 401 402 407 408 409 410 411 416 417 501 502 507 508 509 510 511 516 517".Split(' ');
            string[] room8 = "103 104 105 106 112 113 114 115 203 204 205 206 212 213 214 215 303 304 305 306 312 313 314 315 403 404 405 406 412 413 414 415 503 504 505 506 512 513 514 515".Split(' ');
            string[] not_room = "102 107 108".Split(' ');

            if (room10.Contains(room) && room9.Count(s => s.Equals(room)) < 10)
            {
                return room;
            }
            else if (room8.Contains(room) && room9.Count(s => s.Equals(room)) < 8)
            {
                return room;
            }
            else if (not_room.Contains(room))
            {
                return "zzz";
            }
            else if (!room10.Contains(room) && !room8.Contains(room) && !not_room.Contains(room))
            {
                return "aaa";
            }
            else { return "xxx"; }
            //show_1d_array(room9);
        }
        /// <summary>
        /// Create a 2d Array to take info of people want stay in this dorm, Name, AGe, Class, SSN, ,,
        /// </summary>
        /// <returns></returns>
        static string[,] Take_info()                                                          //Take_info
        {
            string path = "C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv.txt";
            //PreLoad
            string[] sv_name = name(path);
            string[] sv_ssn = ssn(path);
            string[,] sv;
            string[] sv2 = {"  Enter your name : ",
                            "  Enter your age (above 18): ",
                            "  Enter your code class : ",
                            "  Enter your SSN (12 number): "};
            string num2;
            byte num;
            while (true)
            {
                Console.Write("  Enter number of people: ");
                num2 = Console.ReadLine();
                if (byte.TryParse(num2,out num))
                {
                    break;
                }
                else { Console.WriteLine("  Try Again !"); }
                   
            }
            sv = new string[num, 5];
            //Check and take info people
            Console.WriteLine("  The first one !");
            for (byte i = 0; i < num; i++)
            {
                //  check name
                while (true)
                {
                    Console.Write(sv2[0]);
                    string name = Console.ReadLine();
                    if (sv_name.Contains(name) )
                    {
                        Console.WriteLine("\tYour name already exists in this list !");
                    }
                    else if (name.Length > 13)
                    {
                        Console.Write("\tThe name can't be over 13 characters");
                    }
                    else if (name.Any(char.IsDigit))
                    {
                        Console.WriteLine("\tThe name not containt number !");
                    }
                    else if (name.Length < 1)
                    {
                        Console.WriteLine("\tThe name can not below one character !");
                    }
                    else
                    {
                        sv[i, 0] = name;
                        break;
                    }
                }
                //check age
                while (true)
                {
                    byte age;
                    Console.Write(sv2[1]);
                    bool check = byte.TryParse(Console.ReadLine(), out age);

                    if (!check)
                    {
                        Console.WriteLine("\tThe age not contain character !");
                    }
                    else if (age < 18 || age > 26)
                    {
                        Console.WriteLine("\tThe age can't be over 26 or below  18 !");
                    }
                    else
                    {
                        sv[i, 1] = Convert.ToString(age);
                        break;
                    }
                }
                //check class
                while (true)
                {
                    Console.Write(sv2[2]);
                    string code_class = Console.ReadLine();
                    if (code_class.Length == 6 && code_class.All(char.IsDigit))
                    {
                        sv[i, 2] = code_class;
                        break;
                    }
                    else if (code_class.Length == 7 && code_class.EndsWith("TN"))
                    {
                        sv[i, 2] = code_class;
                        break;
                    }
                    else { Console.WriteLine("\tValid code class : \n\t  6 number (xxxxxx)\n\t  7 number(xxxxxTN)"); }
                }
                while (true)
                {
                    Console.Write(sv2[3]);
                    string ssn = Console.ReadLine();
                    if (sv_ssn.Contains(ssn))
                    {
                        Console.WriteLine("\tThis ssn have been exist in this list !");
                    }
                    else if (ssn.All(char.IsDigit) && ssn.Length == 12)
                    {
                        sv[i, 3] = ssn;
                        break;
                    }
                    else { Console.WriteLine("\tAccept only number and lenght is 12 !"); }

                }
                //check room code
                while (true)
                {
                    Console.Write("  ENter your room: ");
                    string room = Console.ReadLine();
                    string check7 = Check_room(room);
                    if (check7 == "xxx")
                    {
                        Console.WriteLine("\tThis room is full people!");
                    }
                    else if (check7 == "zzz")
                    {
                        Console.WriteLine("\tThis room is for management !");
                    }
                    else if (check7 == "aaa")
                    {
                        Console.WriteLine("\tThis room is not valid !");
                    }
                    else
                    {
                        DateTime now = DateTime.Now;
                        Console.WriteLine("\tAccepted !");
                        sv[i, 4] = check7 + "     " + now;
                        break;
                    }
                }
                if (i == num - 1)
                {
                    Console.WriteLine("  Done !");
                }
                else
                {
                    Console.Write("  The next one ! \n");
                }

            } //end for loop

            return sv;
        } //end func
        /// <summary>
        /// Just Write a 2d_array into a file   , Take 2d array as parameter
        /// </summary>
        /// <param name="sv"></param>
        static void Write_Array_to_file(string[,] sv)                          //Write_array_to_file
        {
            //Create a stream overwrite file
            StreamWriter write_file = new StreamWriter("C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv.txt", true);
            //check the length of this 2darray
            int len_sv = sv.Length / 5;
            for (byte i = 0; i < len_sv; i++)
            {
                write_file.Write(sv[i, 0] + ',' + sv[i, 1] + ',' + sv[i, 2] + ',' + sv[i, 3] + ',' + sv[i, 4] + "\n");
            }
            write_file.Close();
        }
        /// <summary>
        /// Like this funtion above just write not over
        /// </summary>
        /// <param name="sv"></param>
        static void Write_Array_to_file_after_delete(string[,] sv)                          //Write_array_to_file
        {
            //Create a stream overwrite file
            StreamWriter write_file = new StreamWriter(
            "C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv.txt", false);
            //check the length of this 2darray
            int len_sv = sv.Length / 5;
            for (byte i = 0; i < len_sv; i++)
            {
                write_file.Write(sv[i, 0] + ',' + sv[i, 1] + ',' + sv[i, 2] + ',' + sv[i, 3] + ',' + sv[i, 4] + "\n");
            }

            write_file.Close();
        }
        /// <summary>
        /// also overwrite but not overwrite that one above, it's recovery
        /// </summary>
        /// <param name="sv"></param>
        static void Write_Array_to_file_after_recover(string[,] sv)                          //Write_array_to_file
        {

            StreamWriter write_file = new StreamWriter(
            "C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv.txt", false);
            //check the length of this 2darray
            int len_sv = sv.Length / 5;
            for (byte i = 0; i < len_sv; i++)
            {
                write_file.Write(sv[i, 0] + ',' +
                                 sv[i, 1] + ',' +
                                 sv[i, 2] + ',' +
                                 sv[i, 3] + ',' +
                                 sv[i, 4] + "\n");
            }

            write_file.Close();
        }
        /// <summary>
        /// just write like that one above          , but after sort not recovery or update
        /// </summary>
        /// <param name="sv"></param>
        static void Write_Array_to_file_after_sort(string[,] sv)                          //Write_array_to_file
        {
            //Create a stream overwrite file
            StreamWriter write_file = new StreamWriter(
            "C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv.txt", false);
            //check the length of this 2darray
            int len_sv = sv.Length / 5;
            for (byte i = 0; i < len_sv; i++)
            {
                write_file.Write(sv[i, 0] + ',' + sv[i, 1] + ',' + sv[i, 2] + ',' + sv[i, 3] + ',' + sv[i, 4] + "\n");
            }

            write_file.Close();
        }

        /// <summary>
        /// show all data of this array, as a show function
        /// </summary>
        /// <param name="sv"></param>
        static void Show_all_data(string[,] sv2)                                 //Show_all_data
        {
            int maxLength = 15;
            //Console.WriteLine(maxLength);
            //Can le phai
            Console.WriteLine("\n\tThis's the list info of all people in this dorm !\n");
            Console.WriteLine("   Name".PadRight(maxLength) +
                              "  Age".PadRight(maxLength) +
                              "  Class".PadRight(maxLength) +
                              "  SSN".PadRight(maxLength) +
                              "  Room".PadRight(maxLength - 6) +
                              " Time");
            for (int i = 0; i < sv2.Length / 5; i++)
            {
                Console.WriteLine("  "+sv2[i, 0].PadRight(maxLength) +
                    sv2[i, 1].PadRight(maxLength) +
                    sv2[i, 2].PadRight(maxLength) +
                    sv2[i, 3].PadRight(maxLength) +
                    sv2[i, 4]);
            }
            Console.WriteLine($"\n\tTable info : {sv2.GetLength(0) - 1}(row),{sv2.GetLength(1)}(column)");
        }
        /// <summary>
        /// del array, acctually , write a base string to mark it and ready to overwrite
        /// </summary>
        /// <param name="path"></param>
        static void Del_array(string path)                                                    //Delete
        {
            StreamWriter del = new StreamWriter(path);
            Console.WriteLine("You want delete this Data set, No Way !" +
                "\nKeep your mind clear boy , once go never come (y/n): ");


            if (Console.ReadLine() == "y")
            {
                del.WriteLine("____________,_________,________,____________,____");      //this is the root of all problem
                del.Close();
                Console.WriteLine("Oh, you have my respect, Boy ");
                Console.Write("Continue your work, right !..");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("I know, Don't be stupid, I like this guy !");
                Console.Write("Tap anywhere to skip !");
                Console.ReadKey();
            }

        }
        /// <summary>
        /// A good Greet for you, My sir !
        /// </summary>
        /// <returns></returns>
        static string Check()                                                                 //check option
        {
            Console.Write("\n  ^*******************************************************^\n" +
                            "  ^**** Dormitory Console Applications for Management ****^\n" +
                            "  ^*******************************************************^\n" +
                            "  ** Menu:  \n" +
                            "\t     1,/Add/".PadRight(19) + "4,/Delete/\n" +
                            "\t     2,/Find/".PadRight(19) + "5,/Sort by alphabet/\n" +
                            "\t     3,/Update/".PadRight(19) + "6,/Show and Statistic/\n" +
                            "\t     7,/Exit/".PadRight(19) + "8,/Obout us/\n" +
                            "\t\t 9,/Backup and. Recovery/\n" +
                            " >> Option : ");
            string check = Console.ReadLine();
            return check;
        }

        static void Take_and_write()                                              //Take and write info into file, 2 in 1
        {
            string[,] sv = Take_info();
            Write_Array_to_file(sv);
        }
        /// <summary>
        /// Read and show , merge funtion
        /// </summary>
        /// <param name="path"></param>
        static void Reload_and_show(string path)                                         //reload and show all data
        {
            string[,] sv2 = ReLoad_Read_file(path);
            Show_all_data(sv2);
        }
        /// <summary>
        /// A funny function to write word slowly
        /// </summary>
        /// <param name="story"></param>
        static void write_slowly(string story)                                                 //Write slowly
        {
            foreach (char y in story)
            {
                Console.Write(y);
                Thread.Sleep(55);
            }
        }
        /// <summary>
        /// find function , find info someone by their name
        /// </summary>
        /// <param name="path"></param>
        /// <param name="key_word"></param>
        /// <param name="col"></param>
        static void Find_(string path, string key_word, int col)                              //Find by detail info
        {
            byte els = 0;
            string[,] sv2 = ReLoad_Read_file(path);
            for (int i = 0; i < sv2.Length / 5; i++)
            {

                if (sv2[i, col] == key_word)
                {
                    Console.Write($"  Yeah !, I've found info about him ,{sv2[i, col]}" +
                        $"\n  You wanna see it : (y/n) ");
                    if (Console.ReadLine() == "y")
                    {
                        string story = $"\n  His name is {sv2[i, 0]} , Now he's {sv2[i, 1]} year old " +
                                       $"\n  And He's staying in room {sv2[i, 4].Split(' ')[0]}" +
                                       $"\n  His SSN is {sv2[i, 3]}" +
                                       $"\n  Student of {sv2[i, 2]} class !" +
                                       $"\n   SO that's all infomation about him !\n";
                        write_slowly(story);
                    }
                    else { Console.WriteLine("  Next !"); }
                }
                else
                {
                    els++;
                }
            }
            if (els == sv2.Length / 5)
            {
                Console.WriteLine("Opps , I have no information about him !");
            }
        }
        //
        static void Find_info(string path)                                                        //Find_info
        {
            Console.Write("  You want find info by what:" +
            "\n  1,/Name/" + "\t2,Age(Not Supported)" + 
            "\n  3,/SSN/" +  "\t4,Room(Not supported)" + 
            "\n  5,Class(Not Supported)" +
            "\n >>Options : ");
            string check;
            while (true)
            {
                check = Console.ReadLine();
                if (check == "1")
                {
                    Console.Write("  Give me the name: ");
                    string name = Console.ReadLine();
                    Find_(path, name, 0);
                    break;
                }
                else if (check == "3")
                {
                    Console.Write("  Your SSN");
                    string ssn = Console.ReadLine();
                    Find_(path, ssn, 3);
                    break;
                }
                else if (check == "5")
                {
                    Console.WriteLine("\tNot supported !");

                }
                else { Console.WriteLine("  Try Again !"); }
            }

        }

        ///sumary
        /// Update info on someone by their name
        /// </summary>
        /// <param name="path"></param>
        /// <param name="key_word"></param>
        static void Update(string path, string key_word)                              //Update
        {
            byte els = 0;
            //PreLoad
            string[] sv_name = name(path);
            string[] sv_ssn = ssn(path);
            string[] sv4 =  {"  Enter your new age (above 18): ",
                            "  Enter your new class : ",
                            "  Enter your new SSN (12 number): "};
            string[,] sv2 = ReLoad_Read_file(path);
            for (int i = 0; i < sv2.Length / 5; i++)
            {
                if (sv2[i, 0] == key_word)
                {
                    Console.Write($"  You mean {sv2[i, 0]} with ssn is {sv2[i, 3]}" +
                                 "\n  Update now (y/n) ");
                    if (Console.ReadLine() == "y")
                    {
                        //check age
                        while (true)
                        {
                            byte age;
                            Console.Write(sv4[0]);
                            bool check = byte.TryParse(Console.ReadLine(), out age);
                            if (!check)
                            {
                                Console.WriteLine("\tThe age not contain character !");
                            }
                            else if (age < 18 || age > 26)
                            {
                                Console.WriteLine("\tThe age can't be over 26 or below  18 !");
                            }
                            else
                            {
                                sv2[i, 1] = Convert.ToString(age);
                                break;
                            }
                        }
                        //check class
                        while (true)
                        {
                            Console.Write(sv4[1]);
                            string code_class = Console.ReadLine();
                            if (code_class.Length == 6 && code_class.All(char.IsDigit))
                            {
                                sv2[i, 2] = code_class;
                                break;
                            }
                            else if (code_class.Length == 7 && code_class.EndsWith("TN"))
                            {
                                sv2[i, 2] = code_class;
                                break;
                            }
                            else { Console.WriteLine("\tValid code class : \n\t  6 number (xxxxxx)\n\t  7 number(xxxxxTN)"); }
                        }
                        while (true)
                        {
                            Console.Write(sv4[2]);
                            string ssn = Console.ReadLine();
                            if (sv_ssn.Contains(ssn))
                            {
                                Console.WriteLine("This ssn have been exist in this list !");
                            }
                            else if (ssn.All(char.IsDigit) && ssn.Length == 12)
                            {
                                sv2[i, 3] = ssn;
                                break;
                            }
                            else { Console.WriteLine("\tAccept only number and lenght is 12 !"); }

                        }
                        //check room code
                        while (true)
                        {
                            Console.Write("  ENter your room: ");
                            string room = Console.ReadLine();
                            string check7 = Check_room(room);
                            if (check7 == "xxx")
                            {
                                Console.WriteLine("\tThis room is full people!");
                            }
                            else if (check7 == "zzz")
                            {
                                Console.WriteLine("\tThis room is for management !");
                            }
                            else if (check7 == "aaa")
                            {
                                Console.WriteLine("\tThis room is not valid !");
                            }
                            else
                            {
                                DateTime now = DateTime.Now;
                                Console.WriteLine("\tAccepted !");
                                sv2[i, 4] = check7 + "     " + now;
                                break;
                            }
                        }
                        Console.WriteLine("\n\tYour info have been updated !");
                        //wrrite to file
                        Write_Array_to_file_update(sv2);
                        Console.WriteLine("  Tap to next !");
                        Console.ReadKey();
                    }
                    else { Console.Write("  Waste my time !");Console.ReadKey(); }
                }
                else
                {
                    els++;
                }
            }
            if (els == sv2.Length / 5)
            {
                Console.WriteLine("  This name not exist in my data set !");
            }
        }
        /// <summary>
        /// Write  after update
        /// </summary>
        /// <param name="sv"></param>
        static void Write_Array_to_file_update(string[,] sv)                          //Write_array_to_file
        {
            //Create a stream overwrite file
            StreamWriter write_file = new StreamWriter(
            "C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv.txt", false);
            //check the length of this 2darray
            int len_sv = sv.Length / 5;
            for (byte i = 0; i < len_sv; i++)
            {
                write_file.Write(sv[i, 0] + ',' + sv[i, 1] + ',' + sv[i, 2] + ',' + sv[i, 3] + ',' + sv[i, 4] + "\n");
            }
            write_file.Close();
        }
        /// <summary>
        /// Check update info , support for update function
        /// </summary>
        /// <param name="path"></param>
        static void Check_Update_info(string path)                                                        //Update info
        {
            Console.Write("  Give me the name: ");
            string name = Console.ReadLine();
            Update(path, name);
            
        }
        static string[,] delete_by_name(string[,] name_before)                //delete by name
        {
            //lấy phần tử thứ nhất làm 1 cái array để so sánh
            string[] name3 = new string[name_before.Length / 5];
            for (int i = 0; i < name_before.Length / 5; i++)
            {
                name3[i] = name_before[i, 0];
            }
            //array after delete one 
            string[,] name_after = new string[name_before.Length / 5 - 1, 5];
            Console.Write("  Enter youu name: ");
            string key = Console.ReadLine();
            if (name3.Contains(key))
            {
                int k = 0;
                for (int o = 0; o < name_after.Length / 5; o++)
                {
                    if (name3[o] == key || k >= 1)
                    {
                        name_after[o, 0] = name_before[o + 1, 0];
                        name_after[o, 1] = name_before[o + 1, 1];
                        name_after[o, 2] = name_before[o + 1, 2];
                        name_after[o, 3] = name_before[o + 1, 3];
                        name_after[o, 4] = name_before[o + 1, 4];
                        k += 1;
                    }
                    else
                    {
                        name_after[o, 0] = name_before[o, 0];
                        name_after[o, 1] = name_before[o, 1];
                        name_after[o, 2] = name_before[o, 2];
                        name_after[o, 3] = name_before[o, 3];
                        name_after[o, 4] = name_before[o, 4];
                    }
                }
                Console.WriteLine($"  Info of {key} have been delete ,I don't talk too more !");
                Console.ReadKey();
                return name_after;
            }
            else
            {
                Console.Write("  This name not exist in this list ! ");
                Console.ReadKey();
                return name_before;
            }
        }
        static void delele_by_name_and_write_file(string path)            //delete and write into file
        {
            string[,] sv6 = ReLoad_Read_file(path);
            string[,] sv7 = delete_by_name(sv6);
            Write_Array_to_file_after_delete(sv7);
        }
        static string[] name_sorted(string path)                               //preload  name col
        {
            string[,] sv2 = ReLoad_Read_file(path);
            string[] sv2_name = new string[sv2.Length / 5];
            for (int i = 0; i < sv2_name.Length; i++)
            {
                sv2_name[i] = sv2[i, 0];
            }
            Array.Sort(sv2_name);
            return sv2_name;
        }
        /// <summary>
        /// </summary>
        /// <param name="sv5"></param>
        /// <param name="name_storted"></param>
        /// <returns></returns>
        static string[,] sort_sv_by_name(string[,] sv5, string[] name_storted)
        {
            string[,] sv4 = new string[sv5.Length / 5, 5];

            for (int i = 0; i < sv4.Length / 5; i++)
            {
                for (int k = 0; k < sv4.Length / 5; k++)
                {
                    if (name_storted[i] == sv5[k, 0])
                    {
                        sv4[i, 0] = sv5[k, 0];
                        sv4[i, 1] = sv5[k, 1];
                        sv4[i, 2] = sv5[k, 2];
                        sv4[i, 3] = sv5[k, 3];
                        sv4[i, 4] = sv5[k, 4];
                    }
                }
            }
            return sv4;
        }
        /// <summary>
        /// read file and show file backup 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        static string[,] ReLoad_and_show_file_backup(string path, string path2)                      // read and show file backup
        {
            string[] line1, line2, line3, his1, reco;
            //Create readstream
            StreamReader read_file = new StreamReader(path);
            StreamReader read_file_his = new StreamReader(path2);
            string all_line = read_file.ReadToEnd().Trim();
            string all_line_his = read_file_his.ReadToEnd().Trim();
            read_file.Close();

            //Show backup data and history backup
            line1 = all_line.Split('/');
            his1 = all_line_his.Split('\n');
            Console.WriteLine("    Backup history !\n");
            for (byte i = 1; i < line1.Length; i++)
            {
                Console.Write($"  ({i}) Data backup at ");
                Console.WriteLine(his1[i - 1] + '\n');

                line1[i] = line1[i].Trim();
                line2 = line1[i].Split('\n');
                for (byte k = 1; k < line2.Length; k++)
                {
                    line2[k] = line2[k].Trim();
                    line3 = line2[k].Split(',');
                    Console.WriteLine($"\t/{line3[0]}/{line3[1]}/{line3[2]}/{line3[3]}/{line3[4]} ");

                }
            }
            Console.WriteLine();
            byte code;
            string code1;
            while (true)
            {
                Console.Write("  Enter the backup code to restore data ! ");
                code1 = Console.ReadLine();
                if (byte.TryParse(code1, out code) && code <= his1.Length )
                {
                    break;
                }
                else { Console.WriteLine("  Try Again !"); }
            }
            reco = line1[code].Trim().Split('\n');
            string[,] recover = new string[reco.Length, 5];
            for (byte l = 0; l < reco.Length; l++)
            {

                recover[l, 0] = reco[l].Split(',')[0].Trim();
                recover[l, 1] = reco[l].Split(',')[1].Trim();
                recover[l, 2] = reco[l].Split(',')[2].Trim();
                recover[l, 3] = reco[l].Split(',')[3].Trim();
                recover[l, 4] = reco[l].Split(',')[4].Trim();
            }
            //Console.WriteLine("recovery array !");
            return recover;

        }

        static void Write_file_backup(string[,] sv)                          //Write_array_to_file backup
        {
            //Create a stream overwrite file
            StreamWriter write_file = new StreamWriter("C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv_backup.txt", true);
            StreamWriter write_file_history = new StreamWriter("C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv_backup_history.txt", true);
            //check the length of this 2darray
            DateTime now = DateTime.Now;
            int len_sv = sv.Length / 5;
            write_file.Write("/");
            for (byte i = 0; i < len_sv; i++)
            {
                write_file.Write(sv[i, 0] + ',' + sv[i, 1] + ',' + sv[i, 2] + ',' + sv[i, 3] + ',' + sv[i, 4] + '\n');
            }
            write_file_history.WriteLine(now);
            //write_file_history.Write("\n");
            write_file.Close();
            write_file_history.Close();
        }
        static string[] code_room2()                  //return room  support for check room
        {
            string path =
            "C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv.txt";
            string[,] sv2 = ReLoad_Read_file(path);
            string[] room9 = new string[sv2.Length / 5];
            //Console.WriteLine("lengh of sv2 " + sv2.Length/5);
            //show_2darray(sv2);
            for (int k = 0; k < room9.Length; k++)
            {
                room9[k] = sv2[k, 4].Split(' ')[0];

            }
            //show_1d_array(room9);
            return room9;
        }

        static void Statistic(string[,] array)                       //Statistic
        {
            Console.WriteLine("\n\t\tStatistic Quantity of each room ");
            string[] room = code_room2();
            //take unik element
            string[] unik = room.Distinct().ToArray();
            Array.Sort(unik);
            int[] unik_sl = new int[unik.Length];
            for (byte i = 0; i < unik.Length; i++)
            {
                unik_sl[i] = room.Count(s => s.Equals(unik[i]));
            }
            //Showw statistic
            //Console.WriteLine(array.Length/5);
            //Console.WriteLine(room.Length);
            //Console.WriteLine(unik.Length); 
            //Console.WriteLine(unik_sl.Length);

            Console.WriteLine("\n\tInd   Room\tQuantity    Member");
            Console.WriteLine("\t _    ____\t________    ______");
            for (byte k = 1; k < unik.Length; k++)
            {
                Console.Write($"\t {k}".PadRight(7) +$"{unik[k]}\t   {unik_sl[k]}        ");
                for (int o = 1; o < array.Length / 5; o++)
                {
                    if (unik[k] == array[o, 4].Split(' ')[0])
                    {
                        Console.Write(array[o, 0] + ',' + ' ');
                    }
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Our main actor, control all of these function 
        /// </summary>
        /// <param name="lovecrush"></param>
        static void Main(string[] lovecrush)                                         //Main
        {
            //Encoding
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            // Start
            string path = "C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv.txt";
            string path_his = "C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv_backup_history.txt";
            string path_backup = "C:/Users/my pc/Documents/file C#/Dormitory console 0.0.2/sv_backup.txt";

            string check;
            //string permit = Greet();

            while (true)
            {
                //Check menu               
                check = Check();
                if (check == "1")
                {
                    Take_and_write();
                    while (true)
                    {
                        Console.Write("  Continue taking info...(y/n) ");
                        if (Console.ReadLine() == "y")
                        {
                            Take_and_write();
                        }
                        else { break; }
                    }
                    Console.Clear();
                }
                else if (check == "2")
                {
                    //option 2
                    Find_info(path);
                    while (true)
                    {
                        Console.Write("\tContinue finding someone...or return Menu (y/n) ");
                        if (Console.ReadLine() == "y")
                        {
                            Find_info(path);
                        }
                        else { break; }
                    }
                    Console.Clear();
                }
                else if (check == "3")
                {
                    Check_Update_info(path);
                    Console.Clear();
                }
                else if (check == "4")
                {
                    //option 4
                    Console.WriteLine("  You have two choice !" +
                                    "\n\t 1) delete all data" +
                                    "\n\t 2) delete one record");
                    Console.Write(" >>Option : ");
                    string check1 = Console.ReadLine();
                    if (check1 == "1")
                    {
                        Del_array(path);
                    }
                    else if (check1 == "2")
                    {
                        Console.WriteLine("  Delete info by name");
                        delele_by_name_and_write_file(path);
                        while (true)
                        {
                            Console.Write("\n  Continue delete someone...or return Menu (y/n) ");
                            if (Console.ReadLine() == "y")
                            {
                                delele_by_name_and_write_file(path);
                            }
                            else { break; }
                        }
                    }
                    Console.Clear();
                }
                else if (check == "5")
                {
                    string[,] sv4 = ReLoad_Read_file(path);
                    string[] name2 = name_sorted(path);
                    Console.Write("  Your list will be sort a-z by name !\n" +
                        "  Are you sure, (y/n) ");
                    if ( Console.ReadLine() == "y")
                    {
                        string[,] name4 = sort_sv_by_name(sv4, name2);
                        Write_Array_to_file_after_sort(name4);
                        Console.Write("  Your list have been sort by a-z, \n  Check with show option !");
                        Console.ReadKey();
                    }
                    Console.Clear();
                }
                else if (check == "6")
                {
                    //option 6
                    Reload_and_show(path);
                    Console.Write("\n    You want statistic now ! (y/n) ");
                    if (Console.ReadLine() == "y")
                    {
                        string[,] sv3 = ReLoad_Read_file(path);
                        Statistic(sv3);
                    }
                    else { Console.WriteLine("   Oke Sir !"); }
                    Console.Write("\n  Tap anywhere to skip !");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (check == "7")
                {
                    //option 7
                    Console.WriteLine("\n\t  Goodbye SIR !");
                    Console.Write("\tHave a nice day !\n    ");
                    break;
                }
                else if (check == "8")
                {
                    Console.Write(  "\n\tOur console-based dormitory management program" +
                                    "\n       is designed to make life easier for dorm managers and students alike." +
                                    "\n      With the ability to add, edit, delete, update, search, and sort records, " +
                                    "\n      managing dormitory information has never been easier or more efficient. " +
                                    "\n      Our program also includes backup and recovery features ensuring that all data is safe and secure." +
                                    "\n      Our program was created by Lovecrush, a team of dedicated developers " +
                                    "\n      committed to providing innovative solutions for everyday problems. " +
                                    "\n      We believe that our program is the ideal choice for dorm managers " +
                                    "\n      looking to streamline their operations and ensure that students are comfortable and happy in " +
                                    "\n      their living environment.\n\n  Tap enywhere to skip !");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (check == "9")
                {
                    Console.WriteLine("\tBackup(b) and Recovery(v) ");
                    Console.Write("  You want backup or recovery :(b/v) ");
                    string check5 = Console.ReadLine();
                    Console.WriteLine();
                    if (check5 == "b")
                    {
                        Console.WriteLine("\tBackup");
                        string[,] data_backup = ReLoad_Read_file(path);
                        Console.Write("  Are you sure : (y/n) ");
                        if (Console.ReadLine() == "y")
                        {
                            Write_file_backup(data_backup);
                            Console.Write("\n   Backup complete ! ");
                        }
                        else { Console.Write("  Not respond ! "); }
                        Console.ReadKey();
                    }
                    else if (check5 == "v")
                    {
                        Console.WriteLine("\tRecovery\n");
                        string[,] recover = ReLoad_and_show_file_backup(path_backup, path_his);
                        Console.Write("  Are you sure , data will be recovery as your data set !(Y/n) ");
                        if (Console.ReadLine() == "y")
                        {
                            Write_Array_to_file_after_recover(recover); Console.Write("\n\tRecovery Done !");
                        }
                        else { Console.Write("Not respond !"); };
                        Console.ReadKey();
                    }
                    Console.Clear();
                }
                else                           
                {
                    Console.Write("\tChoose an option by enter number from 1 to 9\n     You got it, Right !\t");
                    Console.ReadKey();  
                    Console.Clear();
                }
                //dung pull xem no nhu nao
                
                //End
            }
        }
    }
}
