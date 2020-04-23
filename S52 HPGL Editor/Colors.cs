using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace S52_HPGL_Editor
{
    class Style
    {

        public static Dictionary<string, Color> S52colors = new Dictionary<string, Color>();

        public static void init() {


            S52colors["NODTA"] = Color.FromArgb(163, 180, 183);
            S52colors["CURSR"] =  Color.FromArgb(235, 125, 54);
            S52colors["CHBLK"] =  Color.FromArgb(7, 7, 7);
            S52colors["CHGRD"] =  Color.FromArgb(125, 137, 140);
            S52colors["CHGRF"] =  Color.FromArgb(163, 180, 183);
            S52colors["CHRED"] =  Color.FromArgb(241, 84, 105);
            S52colors["CHGRN"] =  Color.FromArgb(104, 228, 86);
            S52colors["CHYLW"] =  Color.FromArgb(244, 218, 72);
            S52colors["CHMGD"] =  Color.FromArgb(197, 69, 195);
            S52colors["CHMGF"] =  Color.FromArgb(211, 166, 233);
            S52colors["CHBRN"] =  Color.FromArgb(177, 145, 57);
            S52colors["CHWHT"] =  Color.FromArgb(212, 234, 238);
            S52colors["SCLBR"] =  Color.FromArgb(235, 125, 54);
            S52colors["CHCOR"] =  Color.FromArgb(235, 125, 54);
            S52colors["LITRD"] =  Color.FromArgb(241, 84, 105);
            S52colors["LITGN"] =  Color.FromArgb(104, 228, 86);
            S52colors["LITYW"] =  Color.FromArgb(244, 218, 72);
            S52colors["ISDNG"] =  Color.FromArgb(197, 69, 195);
            S52colors["DNGHL"] =  Color.FromArgb(241, 84, 105);
            S52colors["TRFCD"] =  Color.FromArgb(197, 69, 195);
            S52colors["TRFCF"] =  Color.FromArgb(211, 166, 233);
            S52colors["LANDA"] =  Color.FromArgb(201, 185, 122);
            S52colors["LANDF"] =  Color.FromArgb(139, 102, 31);
            S52colors["CSTLN"] =  Color.FromArgb(82, 90, 92);
            S52colors["SNDG1"] =  Color.FromArgb(125, 137, 140);
            S52colors["SNDG2"] =  Color.FromArgb(7, 7, 7);
            S52colors["DEPSC"] =  Color.FromArgb(82, 90, 92);
            S52colors["DEPCN"] =  Color.FromArgb(125, 137, 140);
            S52colors["DEPDW"] =  Color.FromArgb(212, 234, 238);
            S52colors["DEPMD"] =  Color.FromArgb(186, 213, 225);
            S52colors["DEPMS"] =  Color.FromArgb(152, 197, 242);
            S52colors["DEPVS"] =  Color.FromArgb(115, 182, 239);
            S52colors["DEPIT"] =  Color.FromArgb(131, 178, 149);
            S52colors["RADHI"] =  Color.FromArgb(104, 228, 86);
            S52colors["RADLO"] =  Color.FromArgb(63, 138, 52);
            S52colors["ARPAT"] =  Color.FromArgb(63, 165, 111);
            S52colors["NINFO"] =  Color.FromArgb(235, 125, 54);
            S52colors["RESBL"] =  Color.FromArgb(58, 120, 240);
            S52colors["ADINF"] =  Color.FromArgb(178, 159, 52);
            S52colors["RESGR"] =  Color.FromArgb(125, 137, 140);
            S52colors["SHIPS"] =  Color.FromArgb(7, 7, 7);
            S52colors["PSTRK"] =  Color.FromArgb(7, 7, 7);
            S52colors["SYTRK"] =  Color.FromArgb(125, 137, 140);
            S52colors["PLRTE"] =  Color.FromArgb(220, 64, 37);
            S52colors["APLRT"] =  Color.FromArgb(235, 125, 54);
            S52colors["UINFD"] =  Color.FromArgb(7, 7, 7);
            S52colors["UINFF"] =  Color.FromArgb(125, 137, 140);
            S52colors["UIBCK"] =  Color.FromArgb(212, 234, 238);
            S52colors["UIAFD"] =  Color.FromArgb(115, 182, 239);
            S52colors["UINFR"] =  Color.FromArgb(241, 84, 105);
            S52colors["UINFG"] =  Color.FromArgb(104, 228, 86);
            S52colors["UINFO"] =  Color.FromArgb(235, 125, 54);
            S52colors["UINFB"] =  Color.FromArgb(58, 120, 240);
            S52colors["UINFM"] =  Color.FromArgb(197, 69, 195);
            S52colors["UIBDR"] =  Color.FromArgb(125, 137, 140);
            S52colors["UIAFF"] =  Color.FromArgb(201, 185, 122);
            S52colors["OUTLW"] =  Color.FromArgb(7, 7, 7);
            S52colors["OUTLL"] =  Color.FromArgb(201, 185, 122);
            S52colors["RES01"] =  Color.FromArgb(163, 180, 183);
            S52colors["RES02"] =  Color.FromArgb(163, 180, 183);
            S52colors["RES03"] =  Color.FromArgb(163, 180, 183);
            S52colors["BKAJ1"] =  Color.FromArgb(7, 7, 7);
            S52colors["BKAJ2"] =  Color.FromArgb(35, 39, 40);


        }


    }
}
