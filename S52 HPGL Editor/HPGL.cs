using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace S52_HPGL_Editor
{
    class HPGL
    {

        public static bool parseSymbol(string content, ref Symbol sym)
        {

            sym = new Symbol();
            string def_name = "SYMD";

            foreach (var line in content.Split('\n'))
            {
                if (line.StartsWith("SYMB"))
                {
                    sym.type = 'S';
                    sym.symb = line;
                }
                else if (line.StartsWith("LNST"))
                {
                    sym.type = 'L';
                    def_name = "LIND";
                    sym.symb = line;
                }
                else if (line.StartsWith("PATT"))
                {
                    sym.type = 'P';
                    def_name = "PATD";
                    sym.symb = line;
                }

                else if (line.StartsWith(sym.type + "XPO"))
                {

                    sym.expo = line.Substring(9);


                }

                else if (line.StartsWith(def_name))
                {

                    int def_index = 9; // Where the symbol properties starts
                    if (sym.type == 'L')
                        def_index = 8;

                    var SYMD = line.Substring(9);

                    var name = SYMD.Substring(0, 8);
                    var type = SYMD.Substring(8, 1);

                    var definition = SYMD.Substring(def_index);
                    sym.name = name;

                    if (sym.type != 'P')
                    {

                        sym.pivot_x = int.Parse(definition.Substring(0, 5));
                        sym.pivot_y = int.Parse(definition.Substring(5, 5));
                        sym.width = int.Parse(definition.Substring(10, 5));
                        sym.height = int.Parse(definition.Substring(15, 5));
                        sym.offset_x = int.Parse(definition.Substring(20, 5));
                        sym.offset_y = int.Parse(definition.Substring(25, 5));
                    }
                    else {
                        sym.pivot_x = int.Parse(definition.Substring(16, 5));
                        sym.pivot_y = int.Parse(definition.Substring(21, 5));
                        sym.width = int.Parse(definition.Substring(26, 5));
                        sym.height = int.Parse(definition.Substring(31, 5));
                        sym.offset_x = int.Parse(definition.Substring(36, 5));
                        sym.offset_y = int.Parse(definition.Substring(41, 5));              

                    }


                }

                else if (line.StartsWith(sym.type + "CRF"))
                {

                    sym.colref = line.Substring(9);
                    Console.WriteLine(sym.colref);

                }

                else if (line.StartsWith(sym.type + "VCT"))
                {

                    sym.instruction += line.Substring(9);


                }



            }

            return true;

        }


        static Color getColor(string colorCode, string col)
        {


            int noColors = col.Length / 6;
            for (int i = 0, j = 0; i < noColors; i++, j += 6)
            {
                if (col.Substring(j, 1) == colorCode) return Style.S52colors[col.Substring(j + 1, 5)];
            }
            return Style.S52colors[col + 1]; // Default to first color if not found.

        }

        static string getColorName(string colorCode, string col)
        {


            int noColors = col.Length / 6;
            for (int i = 0, j = 0; i < noColors; i++, j += 6)
            {
                if (col.Substring(j, 1) == colorCode) return col.Substring(j + 1, 5);
            }

            return "";

        }

       
        public static void parseGeometry(ref Symbol sym)
        {

            string[] commands = Regex.Replace(sym.instruction, @"\t|\n|\r", "").Split(';'); // sym.instruction.Replace(Environment.NewLine, String.Empty).Split(';');


            Point curpos = new Point();
            Point newpos = new Point();
            Pen curpen = new Pen(Color.Transparent, 1);
            List<Point> polygonbuffer = new List<Point>();
            bool polygonmode = false;
            int transparency = 0;
            string color_name = "";
            int penWidth = 1;
            bool newLine = false;

            var line = new HLineString();

            foreach (string command in commands)
            {
                //Console.WriteLine(command);

                if (command.Length >= 2)
                {
                    string[] points;
                    switch (command.Substring(0, 2))
                    //http://www.informatics-consulting.de/software/plot2emf/p2e_hpgl.htm and http://www.isoplotec.co.jp/HPGL/eHPGL.htm
                    {
                        case "SP": //Select pen
                            color_name = getColorName(command.Substring(2), sym.colref);

                            break;
                        case "SW": //Select width - TODO: not part of the standard, is it really line width
                            penWidth = Convert.ToInt32(command[command.Length - 1].ToString());
                            break;

                        case "ST":

                            transparency = Convert.ToInt32(command.Substring(2));

                            break;

                        case "PU": //Move with pen up
                            points = command.Substring(2).Split(',');
                            for (int i = 0; i < points.Length / 2; i++)
                            {
                                curpos.X = Convert.ToInt32(command.Substring(2).Split(',')[2 * i]);
                                curpos.Y = Convert.ToInt32(command.Substring(2).Split(',')[2 * i + 1]);


                                if (polygonmode)
                                    polygonbuffer.Add(curpos);
                                else
                                {

                                    if (line.points.Count() != 0)
                                    {

                                        sym.geometry.Add(new HLineString(line));
                                        line.points.Clear();
                                    }

                                    line.points.Add(curpos);
                                    line.color = color_name;
                                    line.penWidth = penWidth;
                                    line.transparency = transparency;
                                }

                            }
                            break;
                        case "PD": //Move with pen down
                            if (command.Length > 2)
                            {
                                points = command.Substring(2).Split(',');
                                for (int i = 0; i < points.Length / 2; i++)
                                {
                                    newpos.X = Convert.ToInt32(command.Substring(2).Split(',')[2 * i]);
                                    newpos.Y = Convert.ToInt32(command.Substring(2).Split(',')[2 * i + 1]);


                                    curpos = newpos;
                                    if (polygonmode)
                                        polygonbuffer.Add(curpos);
                                    else
                                    {
                                        newLine = true;
                                        line.points.Add(curpos);

                                    }
                                }
                            }
                            else //Weird, but some symbols have just PD without coordinates so I assume it to be a point
                            {
                                HPoint dot = new HPoint();
                                dot.points.Add(curpos);
                                dot.color = color_name;
                                dot.transparency = transparency;
                                dot.penWidth = penWidth;
                                sym.geometry.Add(dot);
                                line.points.Clear();
                            }
                            break;
                        case "CI": //CI r [, qd]  	Draw Circle
                            int radius = Convert.ToInt32(command.Substring(2));
                            radius = (int)(radius);
                            bool filled = false;
                            if (polygonmode)
                                filled = true;

                            HCircle ci = new HCircle();
                            ci.points.Add(curpos);
                            ci.color = color_name;
                            ci.transparency = transparency;
                            ci.penWidth = penWidth;
                            ci.radius = radius;
                            ci.filled = filled;
                            sym.geometry.Add(ci);

                            polygonbuffer.Clear();
                            line.points.Clear();

                            break;
                        case "PM": //PM [n]  	Polygon mode (HPGL/2)
                            
                            if (command.Substring(2) == "0")
                            {
                                Console.WriteLine(command.Substring(2));
                                polygonmode = !polygonmode;
                                if (polygonmode)
                                {
                                    polygonbuffer.Add(curpos);

                                    if (line.points.Count() != 0)
                                    {

                                        line.points.Clear();
                                    }
                                }
                            }
                            else {
                                polygonmode = false;
   
                            }
                            break;
                        case "FP": //FP  	Filled Polygon (HPGL/2)
                            if (polygonbuffer.Count > 0)
                            {

                                HPolygon poly = new HPolygon();
                                poly.points = new List<Point>(polygonbuffer);

                                poly.color = color_name;
                                poly.transparency = transparency;
                                poly.penWidth = penWidth;
                                sym.geometry.Add(poly);
                                polygonbuffer.Clear();

                            }
                            break;
                        default:
                            Console.WriteLine("HP-GL command {0} unknown.", command);
                            break;
                    }
                }
            }

            if (line.points.Count() != 0)
            {
                sym.geometry.Add(new HLineString(line));
                line.points.Clear();
            }


        }

       

        
    }


}
