using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
	/// <summary>
	/// 汉语拼音。
	/// </summary>
	public class Bopomofo
	{
		/// <summary>
		/// 字符编码。
		/// </summary>
		private static Encoding _Encoding;
		/// <summary>
		/// 高位索引。
		/// </summary>
		private static byte[] _IndexH;
		/// <summary>
		/// 低位索引。
		/// </summary>
		private static byte[] _IndexL;
		/// <summary>
		/// 拼音单词。
		/// </summary>
		private static string[] _Letter;
		/// <summary>
		/// 初始化汉语拼音。
		/// </summary>
		static Bopomofo()
		{
			_Encoding = Encoding.GetEncoding(936);
			_IndexH = new byte[397];
			_IndexL = new byte[397];
			_Letter = new string[397];
			_IndexH[000] = 0xB0; _IndexL[000] = 0xA1; _Letter[000] = "a";
			_IndexH[001] = 0xB0; _IndexL[001] = 0xA3; _Letter[001] = "ai";
			_IndexH[002] = 0xB0; _IndexL[002] = 0xB0; _Letter[002] = "an";
			_IndexH[003] = 0xB0; _IndexL[003] = 0xB9; _Letter[003] = "ang";
			_IndexH[004] = 0xB0; _IndexL[004] = 0xBC; _Letter[004] = "ao";
			_IndexH[005] = 0xB0; _IndexL[005] = 0xC5; _Letter[005] = "ba";
			_IndexH[006] = 0xB0; _IndexL[006] = 0xD7; _Letter[006] = "bai";
			_IndexH[007] = 0xB0; _IndexL[007] = 0xDF; _Letter[007] = "ban";
			_IndexH[008] = 0xB0; _IndexL[008] = 0xEE; _Letter[008] = "bang";
			_IndexH[009] = 0xB0; _IndexL[009] = 0xFA; _Letter[009] = "bao";
			_IndexH[010] = 0xB1; _IndexL[010] = 0xAD; _Letter[010] = "bei";
			_IndexH[011] = 0xB1; _IndexL[011] = 0xBC; _Letter[011] = "ben";
			_IndexH[012] = 0xB1; _IndexL[012] = 0xC0; _Letter[012] = "beng";
			_IndexH[013] = 0xB1; _IndexL[013] = 0xC6; _Letter[013] = "bi";
			_IndexH[014] = 0xB1; _IndexL[014] = 0xDE; _Letter[014] = "bian";
			_IndexH[015] = 0xB1; _IndexL[015] = 0xEA; _Letter[015] = "biao";
			_IndexH[016] = 0xB1; _IndexL[016] = 0xEE; _Letter[016] = "bie";
			_IndexH[017] = 0xB1; _IndexL[017] = 0xF2; _Letter[017] = "bin";
			_IndexH[018] = 0xB1; _IndexL[018] = 0xF8; _Letter[018] = "bing";
			_IndexH[019] = 0xB2; _IndexL[019] = 0xA3; _Letter[019] = "bo";
			_IndexH[020] = 0xB2; _IndexL[020] = 0xB8; _Letter[020] = "bu";
			_IndexH[021] = 0xB2; _IndexL[021] = 0xC1; _Letter[021] = "ca";
			_IndexH[022] = 0xB2; _IndexL[022] = 0xC2; _Letter[022] = "cai";
			_IndexH[023] = 0xB2; _IndexL[023] = 0xCD; _Letter[023] = "can";
			_IndexH[024] = 0xB2; _IndexL[024] = 0xD4; _Letter[024] = "cang";
			_IndexH[025] = 0xB2; _IndexL[025] = 0xD9; _Letter[025] = "cao";
			_IndexH[026] = 0xB2; _IndexL[026] = 0xDE; _Letter[026] = "ce";
			_IndexH[027] = 0xB2; _IndexL[027] = 0xE3; _Letter[027] = "ceng";
			_IndexH[028] = 0xB2; _IndexL[028] = 0xE5; _Letter[028] = "cha";
			_IndexH[029] = 0xB2; _IndexL[029] = 0xF0; _Letter[029] = "chai";
			_IndexH[030] = 0xB2; _IndexL[030] = 0xF3; _Letter[030] = "chan";
			_IndexH[031] = 0xB2; _IndexL[031] = 0xFD; _Letter[031] = "chang";
			_IndexH[032] = 0xB3; _IndexL[032] = 0xAC; _Letter[032] = "chao";
			_IndexH[033] = 0xB3; _IndexL[033] = 0xB5; _Letter[033] = "che";
			_IndexH[034] = 0xB3; _IndexL[034] = 0xBB; _Letter[034] = "chen";
			_IndexH[035] = 0xB3; _IndexL[035] = 0xC5; _Letter[035] = "cheng";
			_IndexH[036] = 0xB3; _IndexL[036] = 0xD4; _Letter[036] = "chi";
			_IndexH[037] = 0xB3; _IndexL[037] = 0xE4; _Letter[037] = "chong";
			_IndexH[038] = 0xB3; _IndexL[038] = 0xE9; _Letter[038] = "chou";
			_IndexH[039] = 0xB3; _IndexL[039] = 0xF5; _Letter[039] = "chu";
			_IndexH[040] = 0xB4; _IndexL[040] = 0xA7; _Letter[040] = "chuai";
			_IndexH[041] = 0xB4; _IndexL[041] = 0xA8; _Letter[041] = "chuan";
			_IndexH[042] = 0xB4; _IndexL[042] = 0xAF; _Letter[042] = "chuang";
			_IndexH[043] = 0xB4; _IndexL[043] = 0xB5; _Letter[043] = "chui";
			_IndexH[044] = 0xB4; _IndexL[044] = 0xBA; _Letter[044] = "chun";
			_IndexH[045] = 0xB4; _IndexL[045] = 0xC1; _Letter[045] = "chuo";
			_IndexH[046] = 0xB4; _IndexL[046] = 0xC3; _Letter[046] = "ci";
			_IndexH[047] = 0xB4; _IndexL[047] = 0xCF; _Letter[047] = "cong";
			_IndexH[048] = 0xB4; _IndexL[048] = 0xD5; _Letter[048] = "cou";
			_IndexH[049] = 0xB4; _IndexL[049] = 0xD6; _Letter[049] = "cu";
			_IndexH[050] = 0xB4; _IndexL[050] = 0xDA; _Letter[050] = "cuan";
			_IndexH[051] = 0xB4; _IndexL[051] = 0xDD; _Letter[051] = "cui";
			_IndexH[052] = 0xB4; _IndexL[052] = 0xE5; _Letter[052] = "cun";
			_IndexH[053] = 0xB4; _IndexL[053] = 0xE8; _Letter[053] = "cuo";
			_IndexH[054] = 0xB4; _IndexL[054] = 0xEE; _Letter[054] = "da";
			_IndexH[055] = 0xB4; _IndexL[055] = 0xF4; _Letter[055] = "dai";
			_IndexH[056] = 0xB5; _IndexL[056] = 0xA2; _Letter[056] = "dan";
			_IndexH[057] = 0xB5; _IndexL[057] = 0xB1; _Letter[057] = "dang";
			_IndexH[058] = 0xB5; _IndexL[058] = 0xB6; _Letter[058] = "dao";
			_IndexH[059] = 0xB5; _IndexL[059] = 0xC2; _Letter[059] = "de";
			_IndexH[060] = 0xB5; _IndexL[060] = 0xC5; _Letter[060] = "deng";
			_IndexH[061] = 0xB5; _IndexL[061] = 0xCC; _Letter[061] = "di";
			_IndexH[062] = 0xB5; _IndexL[062] = 0xDF; _Letter[062] = "dian";
			_IndexH[063] = 0xB5; _IndexL[063] = 0xEF; _Letter[063] = "diao";
			_IndexH[064] = 0xB5; _IndexL[064] = 0xF8; _Letter[064] = "die";
			_IndexH[065] = 0xB6; _IndexL[065] = 0xA1; _Letter[065] = "ding";
			_IndexH[066] = 0xB6; _IndexL[066] = 0xAA; _Letter[066] = "diu";
			_IndexH[067] = 0xB6; _IndexL[067] = 0xAB; _Letter[067] = "dong";
			_IndexH[068] = 0xB6; _IndexL[068] = 0xB5; _Letter[068] = "dou";
			_IndexH[069] = 0xB6; _IndexL[069] = 0xBC; _Letter[069] = "du";
			_IndexH[070] = 0xB6; _IndexL[070] = 0xCB; _Letter[070] = "duan";
			_IndexH[071] = 0xB6; _IndexL[071] = 0xD1; _Letter[071] = "dui";
			_IndexH[072] = 0xB6; _IndexL[072] = 0xD5; _Letter[072] = "dun";
			_IndexH[073] = 0xB6; _IndexL[073] = 0xDE; _Letter[073] = "duo";
			_IndexH[074] = 0xB6; _IndexL[074] = 0xEA; _Letter[074] = "e";
			_IndexH[075] = 0xB6; _IndexL[075] = 0xF7; _Letter[075] = "en";
			_IndexH[076] = 0xB6; _IndexL[076] = 0xF8; _Letter[076] = "er";
			_IndexH[077] = 0xB7; _IndexL[077] = 0xA2; _Letter[077] = "fa";
			_IndexH[078] = 0xB7; _IndexL[078] = 0xAA; _Letter[078] = "fan";
			_IndexH[079] = 0xB7; _IndexL[079] = 0xBB; _Letter[079] = "fang";
			_IndexH[080] = 0xB7; _IndexL[080] = 0xC6; _Letter[080] = "fei";
			_IndexH[081] = 0xB7; _IndexL[081] = 0xD2; _Letter[081] = "fen";
			_IndexH[082] = 0xB7; _IndexL[082] = 0xE1; _Letter[082] = "feng";
			_IndexH[083] = 0xB7; _IndexL[083] = 0xF0; _Letter[083] = "fo";
			_IndexH[084] = 0xB7; _IndexL[084] = 0xF1; _Letter[084] = "fou";
			_IndexH[085] = 0xB7; _IndexL[085] = 0xF2; _Letter[085] = "fu";
			_IndexH[086] = 0xB8; _IndexL[086] = 0xC1; _Letter[086] = "ga";
			_IndexH[087] = 0xB8; _IndexL[087] = 0xC3; _Letter[087] = "gai";
			_IndexH[088] = 0xB8; _IndexL[088] = 0xC9; _Letter[088] = "gan";
			_IndexH[089] = 0xB8; _IndexL[089] = 0xD4; _Letter[089] = "gang";
			_IndexH[090] = 0xB8; _IndexL[090] = 0xDD; _Letter[090] = "gao";
			_IndexH[091] = 0xB8; _IndexL[091] = 0xE7; _Letter[091] = "ge";
			_IndexH[092] = 0xB8; _IndexL[092] = 0xF8; _Letter[092] = "gei";
			_IndexH[093] = 0xB8; _IndexL[093] = 0xF9; _Letter[093] = "gen";
			_IndexH[094] = 0xB8; _IndexL[094] = 0xFB; _Letter[094] = "geng";
			_IndexH[095] = 0xB9; _IndexL[095] = 0xA4; _Letter[095] = "gong";
			_IndexH[096] = 0xB9; _IndexL[096] = 0xB3; _Letter[096] = "gou";
			_IndexH[097] = 0xB9; _IndexL[097] = 0xBC; _Letter[097] = "gu";
			_IndexH[098] = 0xB9; _IndexL[098] = 0xCE; _Letter[098] = "gua";
			_IndexH[099] = 0xB9; _IndexL[099] = 0xD4; _Letter[099] = "guai";
			_IndexH[100] = 0xB9; _IndexL[100] = 0xD7; _Letter[100] = "guan";
			_IndexH[101] = 0xB9; _IndexL[101] = 0xE2; _Letter[101] = "guang";
			_IndexH[102] = 0xB9; _IndexL[102] = 0xE5; _Letter[102] = "gui";
			_IndexH[103] = 0xB9; _IndexL[103] = 0xF5; _Letter[103] = "gun";
			_IndexH[104] = 0xB9; _IndexL[104] = 0xF8; _Letter[104] = "guo";
			_IndexH[105] = 0xB9; _IndexL[105] = 0xFE; _Letter[105] = "ha";
			_IndexH[106] = 0xBA; _IndexL[106] = 0xA1; _Letter[106] = "hai";
			_IndexH[107] = 0xBA; _IndexL[107] = 0xA8; _Letter[107] = "han";
			_IndexH[108] = 0xBA; _IndexL[108] = 0xBB; _Letter[108] = "hang";
			_IndexH[109] = 0xBA; _IndexL[109] = 0xBE; _Letter[109] = "hao";
			_IndexH[110] = 0xBA; _IndexL[110] = 0xC7; _Letter[110] = "he";
			_IndexH[111] = 0xBA; _IndexL[111] = 0xD9; _Letter[111] = "hei";
			_IndexH[112] = 0xBA; _IndexL[112] = 0xDB; _Letter[112] = "hen";
			_IndexH[113] = 0xBA; _IndexL[113] = 0xDF; _Letter[113] = "heng";
			_IndexH[114] = 0xBA; _IndexL[114] = 0xE4; _Letter[114] = "hong";
			_IndexH[115] = 0xBA; _IndexL[115] = 0xED; _Letter[115] = "hou";
			_IndexH[116] = 0xBA; _IndexL[116] = 0xF4; _Letter[116] = "hu";
			_IndexH[117] = 0xBB; _IndexL[117] = 0xA8; _Letter[117] = "hua";
			_IndexH[118] = 0xBB; _IndexL[118] = 0xB1; _Letter[118] = "huai";
			_IndexH[119] = 0xBB; _IndexL[119] = 0xB6; _Letter[119] = "huan";
			_IndexH[120] = 0xBB; _IndexL[120] = 0xC4; _Letter[120] = "huang";
			_IndexH[121] = 0xBB; _IndexL[121] = 0xD2; _Letter[121] = "hui";
			_IndexH[122] = 0xBB; _IndexL[122] = 0xE7; _Letter[122] = "hun";
			_IndexH[123] = 0xBB; _IndexL[123] = 0xED; _Letter[123] = "huo";
			_IndexH[124] = 0xBB; _IndexL[124] = 0xF7; _Letter[124] = "ji";
			_IndexH[125] = 0xBC; _IndexL[125] = 0xCE; _Letter[125] = "jia";
			_IndexH[126] = 0xBC; _IndexL[126] = 0xDF; _Letter[126] = "jian";
			_IndexH[127] = 0xBD; _IndexL[127] = 0xA9; _Letter[127] = "jiang";
			_IndexH[128] = 0xBD; _IndexL[128] = 0xB6; _Letter[128] = "jiao";
			_IndexH[129] = 0xBD; _IndexL[129] = 0xD2; _Letter[129] = "jie";
			_IndexH[130] = 0xBD; _IndexL[130] = 0xED; _Letter[130] = "jin";
			_IndexH[131] = 0xBE; _IndexL[131] = 0xA3; _Letter[131] = "jing";
			_IndexH[132] = 0xBE; _IndexL[132] = 0xBC; _Letter[132] = "jiong";
			_IndexH[133] = 0xBE; _IndexL[133] = 0xBE; _Letter[133] = "jiu";
			_IndexH[134] = 0xBE; _IndexL[134] = 0xCF; _Letter[134] = "ju";
			_IndexH[135] = 0xBE; _IndexL[135] = 0xE8; _Letter[135] = "juan";
			_IndexH[136] = 0xBE; _IndexL[136] = 0xEF; _Letter[136] = "jue";
			_IndexH[137] = 0xBE; _IndexL[137] = 0xF9; _Letter[137] = "jun";
			_IndexH[138] = 0xBF; _IndexL[138] = 0xA6; _Letter[138] = "ka";
			_IndexH[139] = 0xBF; _IndexL[139] = 0xAA; _Letter[139] = "kai";
			_IndexH[140] = 0xBF; _IndexL[140] = 0xAF; _Letter[140] = "kan";
			_IndexH[141] = 0xBF; _IndexL[141] = 0xB5; _Letter[141] = "kang";
			_IndexH[142] = 0xBF; _IndexL[142] = 0xBC; _Letter[142] = "kao";
			_IndexH[143] = 0xBF; _IndexL[143] = 0xC0; _Letter[143] = "ke";
			_IndexH[144] = 0xBF; _IndexL[144] = 0xCF; _Letter[144] = "ken";
			_IndexH[145] = 0xBF; _IndexL[145] = 0xD3; _Letter[145] = "keng";
			_IndexH[146] = 0xBF; _IndexL[146] = 0xD5; _Letter[146] = "kong";
			_IndexH[147] = 0xBF; _IndexL[147] = 0xD9; _Letter[147] = "kou";
			_IndexH[148] = 0xBF; _IndexL[148] = 0xDD; _Letter[148] = "ku";
			_IndexH[149] = 0xBF; _IndexL[149] = 0xE4; _Letter[149] = "kua";
			_IndexH[150] = 0xBF; _IndexL[150] = 0xE9; _Letter[150] = "kuai";
			_IndexH[151] = 0xBF; _IndexL[151] = 0xED; _Letter[151] = "kuan";
			_IndexH[152] = 0xBF; _IndexL[152] = 0xEF; _Letter[152] = "kuang";
			_IndexH[153] = 0xBF; _IndexL[153] = 0xF7; _Letter[153] = "kui";
			_IndexH[154] = 0xC0; _IndexL[154] = 0xA4; _Letter[154] = "kun";
			_IndexH[155] = 0xC0; _IndexL[155] = 0xA8; _Letter[155] = "kuo";
			_IndexH[156] = 0xC0; _IndexL[156] = 0xAC; _Letter[156] = "la";
			_IndexH[157] = 0xC0; _IndexL[157] = 0xB3; _Letter[157] = "lai";
			_IndexH[158] = 0xC0; _IndexL[158] = 0xB6; _Letter[158] = "lan";
			_IndexH[159] = 0xC0; _IndexL[159] = 0xC5; _Letter[159] = "lang";
			_IndexH[160] = 0xC0; _IndexL[160] = 0xCC; _Letter[160] = "lao";
			_IndexH[161] = 0xC0; _IndexL[161] = 0xD5; _Letter[161] = "le";
			_IndexH[162] = 0xC0; _IndexL[162] = 0xD7; _Letter[162] = "lei";
			_IndexH[163] = 0xC0; _IndexL[163] = 0xE2; _Letter[163] = "leng";
			_IndexH[164] = 0xC0; _IndexL[164] = 0xE5; _Letter[164] = "li";
			_IndexH[165] = 0xC1; _IndexL[165] = 0xA9; _Letter[165] = "lia";
			_IndexH[166] = 0xC1; _IndexL[166] = 0xAA; _Letter[166] = "lian";
			_IndexH[167] = 0xC1; _IndexL[167] = 0xB8; _Letter[167] = "liang";
			_IndexH[168] = 0xC1; _IndexL[168] = 0xC3; _Letter[168] = "liao";
			_IndexH[169] = 0xC1; _IndexL[169] = 0xD0; _Letter[169] = "lie";
			_IndexH[170] = 0xC1; _IndexL[170] = 0xD5; _Letter[170] = "lin";
			_IndexH[171] = 0xC1; _IndexL[171] = 0xE1; _Letter[171] = "ling";
			_IndexH[172] = 0xC1; _IndexL[172] = 0xEF; _Letter[172] = "liu";
			_IndexH[173] = 0xC1; _IndexL[173] = 0xFA; _Letter[173] = "long";
			_IndexH[174] = 0xC2; _IndexL[174] = 0xA5; _Letter[174] = "lou";
			_IndexH[175] = 0xC2; _IndexL[175] = 0xAB; _Letter[175] = "lu";
			_IndexH[176] = 0xC2; _IndexL[176] = 0xBF; _Letter[176] = "lv";
			_IndexH[177] = 0xC2; _IndexL[177] = 0xCD; _Letter[177] = "luan";
			_IndexH[178] = 0xC2; _IndexL[178] = 0xD3; _Letter[178] = "lue";
			_IndexH[179] = 0xC2; _IndexL[179] = 0xD5; _Letter[179] = "lun";
			_IndexH[180] = 0xC2; _IndexL[180] = 0xDC; _Letter[180] = "luo";
			_IndexH[181] = 0xC2; _IndexL[181] = 0xE8; _Letter[181] = "ma";
			_IndexH[182] = 0xC2; _IndexL[182] = 0xF1; _Letter[182] = "mai";
			_IndexH[183] = 0xC2; _IndexL[183] = 0xF7; _Letter[183] = "man";
			_IndexH[184] = 0xC3; _IndexL[184] = 0xA2; _Letter[184] = "mang";
			_IndexH[185] = 0xC3; _IndexL[185] = 0xA8; _Letter[185] = "mao";
			_IndexH[186] = 0xC3; _IndexL[186] = 0xB4; _Letter[186] = "me";
			_IndexH[187] = 0xC3; _IndexL[187] = 0xB5; _Letter[187] = "mei";
			_IndexH[188] = 0xC3; _IndexL[188] = 0xC5; _Letter[188] = "men";
			_IndexH[189] = 0xC3; _IndexL[189] = 0xC8; _Letter[189] = "meng";
			_IndexH[190] = 0xC3; _IndexL[190] = 0xD0; _Letter[190] = "mi";
			_IndexH[191] = 0xC3; _IndexL[191] = 0xDE; _Letter[191] = "mian";
			_IndexH[192] = 0xC3; _IndexL[192] = 0xE7; _Letter[192] = "miao";
			_IndexH[193] = 0xC3; _IndexL[193] = 0xEF; _Letter[193] = "mie";
			_IndexH[194] = 0xC3; _IndexL[194] = 0xF1; _Letter[194] = "min";
			_IndexH[195] = 0xC3; _IndexL[195] = 0xF7; _Letter[195] = "ming";
			_IndexH[196] = 0xC3; _IndexL[196] = 0xFD; _Letter[196] = "miu";
			_IndexH[197] = 0xC3; _IndexL[197] = 0xFE; _Letter[197] = "mo";
			_IndexH[198] = 0xC4; _IndexL[198] = 0xB1; _Letter[198] = "mou";
			_IndexH[199] = 0xC4; _IndexL[199] = 0xB4; _Letter[199] = "mu";
			_IndexH[200] = 0xC4; _IndexL[200] = 0xC3; _Letter[200] = "na";
			_IndexH[201] = 0xC4; _IndexL[201] = 0xCA; _Letter[201] = "nai";
			_IndexH[202] = 0xC4; _IndexL[202] = 0xCF; _Letter[202] = "nan";
			_IndexH[203] = 0xC4; _IndexL[203] = 0xD2; _Letter[203] = "nang";
			_IndexH[204] = 0xC4; _IndexL[204] = 0xD3; _Letter[204] = "nao";
			_IndexH[205] = 0xC4; _IndexL[205] = 0xD8; _Letter[205] = "ne";
			_IndexH[206] = 0xC4; _IndexL[206] = 0xD9; _Letter[206] = "nei";
			_IndexH[207] = 0xC4; _IndexL[207] = 0xDB; _Letter[207] = "nen";
			_IndexH[208] = 0xC4; _IndexL[208] = 0xDC; _Letter[208] = "neng";
			_IndexH[209] = 0xC4; _IndexL[209] = 0xDD; _Letter[209] = "ni";
			_IndexH[210] = 0xC4; _IndexL[210] = 0xE8; _Letter[210] = "nian";
			_IndexH[211] = 0xC4; _IndexL[211] = 0xEF; _Letter[211] = "niang";
			_IndexH[212] = 0xC4; _IndexL[212] = 0xF1; _Letter[212] = "niao";
			_IndexH[213] = 0xC4; _IndexL[213] = 0xF3; _Letter[213] = "nie";
			_IndexH[214] = 0xC4; _IndexL[214] = 0xFA; _Letter[214] = "nin";
			_IndexH[215] = 0xC4; _IndexL[215] = 0xFB; _Letter[215] = "ning";
			_IndexH[216] = 0xC5; _IndexL[216] = 0xA3; _Letter[216] = "niu";
			_IndexH[217] = 0xC5; _IndexL[217] = 0xA7; _Letter[217] = "nong";
			_IndexH[218] = 0xC5; _IndexL[218] = 0xAB; _Letter[218] = "nu";
			_IndexH[219] = 0xC5; _IndexL[219] = 0xAE; _Letter[219] = "nv";
			_IndexH[220] = 0xC5; _IndexL[220] = 0xAF; _Letter[220] = "nuan";
			_IndexH[221] = 0xC5; _IndexL[221] = 0xB0; _Letter[221] = "nue";
			_IndexH[222] = 0xC5; _IndexL[222] = 0xB2; _Letter[222] = "nuo";
			_IndexH[223] = 0xC5; _IndexL[223] = 0xB6; _Letter[223] = "o";
			_IndexH[224] = 0xC5; _IndexL[224] = 0xB7; _Letter[224] = "ou";
			_IndexH[225] = 0xC5; _IndexL[225] = 0xBE; _Letter[225] = "pa";
			_IndexH[226] = 0xC5; _IndexL[226] = 0xC4; _Letter[226] = "pai";
			_IndexH[227] = 0xC5; _IndexL[227] = 0xCA; _Letter[227] = "pan";
			_IndexH[228] = 0xC5; _IndexL[228] = 0xD2; _Letter[228] = "pang";
			_IndexH[229] = 0xC5; _IndexL[229] = 0xD7; _Letter[229] = "pao";
			_IndexH[230] = 0xC5; _IndexL[230] = 0xDE; _Letter[230] = "pei";
			_IndexH[231] = 0xC5; _IndexL[231] = 0xE7; _Letter[231] = "pen";
			_IndexH[232] = 0xC5; _IndexL[232] = 0xE9; _Letter[232] = "peng";
			_IndexH[233] = 0xC5; _IndexL[233] = 0xF7; _Letter[233] = "pi";
			_IndexH[234] = 0xC6; _IndexL[234] = 0xAA; _Letter[234] = "pian";
			_IndexH[235] = 0xC6; _IndexL[235] = 0xAE; _Letter[235] = "piao";
			_IndexH[236] = 0xC6; _IndexL[236] = 0xB2; _Letter[236] = "pie";
			_IndexH[237] = 0xC6; _IndexL[237] = 0xB4; _Letter[237] = "pin";
			_IndexH[238] = 0xC6; _IndexL[238] = 0xB9; _Letter[238] = "ping";
			_IndexH[239] = 0xC6; _IndexL[239] = 0xC2; _Letter[239] = "po";
			_IndexH[240] = 0xC6; _IndexL[240] = 0xCB; _Letter[240] = "pu";
			_IndexH[241] = 0xC6; _IndexL[241] = 0xDA; _Letter[241] = "qi";
			_IndexH[242] = 0xC6; _IndexL[242] = 0xFE; _Letter[242] = "qia";
			_IndexH[243] = 0xC7; _IndexL[243] = 0xA3; _Letter[243] = "qian";
			_IndexH[244] = 0xC7; _IndexL[244] = 0xB9; _Letter[244] = "qiang";
			_IndexH[245] = 0xC7; _IndexL[245] = 0xC1; _Letter[245] = "qiao";
			_IndexH[246] = 0xC7; _IndexL[246] = 0xD0; _Letter[246] = "qie";
			_IndexH[247] = 0xC7; _IndexL[247] = 0xD5; _Letter[247] = "qin";
			_IndexH[248] = 0xC7; _IndexL[248] = 0xE0; _Letter[248] = "qing";
			_IndexH[249] = 0xC7; _IndexL[249] = 0xED; _Letter[249] = "qiong";
			_IndexH[250] = 0xC7; _IndexL[250] = 0xEF; _Letter[250] = "qiu";
			_IndexH[251] = 0xC7; _IndexL[251] = 0xF7; _Letter[251] = "qu";
			_IndexH[252] = 0xC8; _IndexL[252] = 0xA6; _Letter[252] = "quan";
			_IndexH[253] = 0xC8; _IndexL[253] = 0xB1; _Letter[253] = "que";
			_IndexH[254] = 0xC8; _IndexL[254] = 0xB9; _Letter[254] = "qun";
			_IndexH[255] = 0xC8; _IndexL[255] = 0xBB; _Letter[255] = "ran";
			_IndexH[256] = 0xC8; _IndexL[256] = 0xBF; _Letter[256] = "rang";
			_IndexH[257] = 0xC8; _IndexL[257] = 0xC4; _Letter[257] = "rao";
			_IndexH[258] = 0xC8; _IndexL[258] = 0xC7; _Letter[258] = "re";
			_IndexH[259] = 0xC8; _IndexL[259] = 0xC9; _Letter[259] = "ren";
			_IndexH[260] = 0xC8; _IndexL[260] = 0xD3; _Letter[260] = "reng";
			_IndexH[261] = 0xC8; _IndexL[261] = 0xD5; _Letter[261] = "ri";
			_IndexH[262] = 0xC8; _IndexL[262] = 0xD6; _Letter[262] = "rong";
			_IndexH[263] = 0xC8; _IndexL[263] = 0xE0; _Letter[263] = "rou";
			_IndexH[264] = 0xC8; _IndexL[264] = 0xE3; _Letter[264] = "ru";
			_IndexH[265] = 0xC8; _IndexL[265] = 0xED; _Letter[265] = "ruan";
			_IndexH[266] = 0xC8; _IndexL[266] = 0xEF; _Letter[266] = "rui";
			_IndexH[267] = 0xC8; _IndexL[267] = 0xF2; _Letter[267] = "run";
			_IndexH[268] = 0xC8; _IndexL[268] = 0xF4; _Letter[268] = "ruo";
			_IndexH[269] = 0xC8; _IndexL[269] = 0xF6; _Letter[269] = "sa";
			_IndexH[270] = 0xC8; _IndexL[270] = 0xF9; _Letter[270] = "sai";
			_IndexH[271] = 0xC8; _IndexL[271] = 0xFD; _Letter[271] = "san";
			_IndexH[272] = 0xC9; _IndexL[272] = 0xA3; _Letter[272] = "sang";
			_IndexH[273] = 0xC9; _IndexL[273] = 0xA6; _Letter[273] = "sao";
			_IndexH[274] = 0xC9; _IndexL[274] = 0xAA; _Letter[274] = "se";
			_IndexH[275] = 0xC9; _IndexL[275] = 0xAD; _Letter[275] = "sen";
			_IndexH[276] = 0xC9; _IndexL[276] = 0xAE; _Letter[276] = "seng";
			_IndexH[277] = 0xC9; _IndexL[277] = 0xAF; _Letter[277] = "sha";
			_IndexH[278] = 0xC9; _IndexL[278] = 0xB8; _Letter[278] = "shai";
			_IndexH[279] = 0xC9; _IndexL[279] = 0xBA; _Letter[279] = "shan";
			_IndexH[280] = 0xC9; _IndexL[280] = 0xCA; _Letter[280] = "shang";
			_IndexH[281] = 0xC9; _IndexL[281] = 0xD2; _Letter[281] = "shao";
			_IndexH[282] = 0xC9; _IndexL[282] = 0xDD; _Letter[282] = "she";
			_IndexH[283] = 0xC9; _IndexL[283] = 0xE9; _Letter[283] = "shen";
			_IndexH[284] = 0xC9; _IndexL[284] = 0xF9; _Letter[284] = "sheng";
			_IndexH[285] = 0xCA; _IndexL[285] = 0xA6; _Letter[285] = "shi";
			_IndexH[286] = 0xCA; _IndexL[286] = 0xD5; _Letter[286] = "shou";
			_IndexH[287] = 0xCA; _IndexL[287] = 0xDF; _Letter[287] = "shu";
			_IndexH[288] = 0xCB; _IndexL[288] = 0xA2; _Letter[288] = "shua";
			_IndexH[289] = 0xCB; _IndexL[289] = 0xA4; _Letter[289] = "shuai";
			_IndexH[290] = 0xCB; _IndexL[290] = 0xA8; _Letter[290] = "shuan";
			_IndexH[291] = 0xCB; _IndexL[291] = 0xAA; _Letter[291] = "shuang";
			_IndexH[292] = 0xCB; _IndexL[292] = 0xAD; _Letter[292] = "shui";
			_IndexH[293] = 0xCB; _IndexL[293] = 0xB1; _Letter[293] = "shun";
			_IndexH[294] = 0xCB; _IndexL[294] = 0xB5; _Letter[294] = "shuo";
			_IndexH[295] = 0xCB; _IndexL[295] = 0xB9; _Letter[295] = "si";
			_IndexH[296] = 0xCB; _IndexL[296] = 0xC9; _Letter[296] = "song";
			_IndexH[297] = 0xCB; _IndexL[297] = 0xD1; _Letter[297] = "sou";
			_IndexH[298] = 0xCB; _IndexL[298] = 0xD4; _Letter[298] = "su";
			_IndexH[299] = 0xCB; _IndexL[299] = 0xE1; _Letter[299] = "suan";
			_IndexH[300] = 0xCB; _IndexL[300] = 0xE4; _Letter[300] = "sui";
			_IndexH[301] = 0xCB; _IndexL[301] = 0xEF; _Letter[301] = "sun";
			_IndexH[302] = 0xCB; _IndexL[302] = 0xF2; _Letter[302] = "suo";
			_IndexH[303] = 0xCB; _IndexL[303] = 0xFA; _Letter[303] = "ta";
			_IndexH[304] = 0xCC; _IndexL[304] = 0xA5; _Letter[304] = "tai";
			_IndexH[305] = 0xCC; _IndexL[305] = 0xAE; _Letter[305] = "tan";
			_IndexH[306] = 0xCC; _IndexL[306] = 0xC0; _Letter[306] = "tang";
			_IndexH[307] = 0xCC; _IndexL[307] = 0xCD; _Letter[307] = "tao";
			_IndexH[308] = 0xCC; _IndexL[308] = 0xD8; _Letter[308] = "te";
			_IndexH[309] = 0xCC; _IndexL[309] = 0xD9; _Letter[309] = "teng";
			_IndexH[310] = 0xCC; _IndexL[310] = 0xDD; _Letter[310] = "ti";
			_IndexH[311] = 0xCC; _IndexL[311] = 0xEC; _Letter[311] = "tian";
			_IndexH[312] = 0xCC; _IndexL[312] = 0xF4; _Letter[312] = "tiao";
			_IndexH[313] = 0xCC; _IndexL[313] = 0xF9; _Letter[313] = "tie";
			_IndexH[314] = 0xCC; _IndexL[314] = 0xFC; _Letter[314] = "ting";
			_IndexH[315] = 0xCD; _IndexL[315] = 0xA8; _Letter[315] = "tong";
			_IndexH[316] = 0xCD; _IndexL[316] = 0xB5; _Letter[316] = "tou";
			_IndexH[317] = 0xCD; _IndexL[317] = 0xB9; _Letter[317] = "tu";
			_IndexH[318] = 0xCD; _IndexL[318] = 0xC4; _Letter[318] = "tuan";
			_IndexH[319] = 0xCD; _IndexL[319] = 0xC6; _Letter[319] = "tui";
			_IndexH[320] = 0xCD; _IndexL[320] = 0xCC; _Letter[320] = "tun";
			_IndexH[321] = 0xCD; _IndexL[321] = 0xCF; _Letter[321] = "tuo";
			_IndexH[322] = 0xCD; _IndexL[322] = 0xDA; _Letter[322] = "wa";
			_IndexH[323] = 0xCD; _IndexL[323] = 0xE1; _Letter[323] = "wai";
			_IndexH[324] = 0xCD; _IndexL[324] = 0xE3; _Letter[324] = "wan";
			_IndexH[325] = 0xCD; _IndexL[325] = 0xF4; _Letter[325] = "wang";
			_IndexH[326] = 0xCD; _IndexL[326] = 0xFE; _Letter[326] = "wei";
			_IndexH[327] = 0xCE; _IndexL[327] = 0xC1; _Letter[327] = "wen";
			_IndexH[328] = 0xCE; _IndexL[328] = 0xCB; _Letter[328] = "weng";
			_IndexH[329] = 0xCE; _IndexL[329] = 0xCE; _Letter[329] = "wo";
			_IndexH[330] = 0xCE; _IndexL[330] = 0xD7; _Letter[330] = "wu";
			_IndexH[331] = 0xCE; _IndexL[331] = 0xF4; _Letter[331] = "xi";
			_IndexH[332] = 0xCF; _IndexL[332] = 0xB9; _Letter[332] = "xia";
			_IndexH[333] = 0xCF; _IndexL[333] = 0xC6; _Letter[333] = "xian";
			_IndexH[334] = 0xCF; _IndexL[334] = 0xE0; _Letter[334] = "xiang";
			_IndexH[335] = 0xCF; _IndexL[335] = 0xF4; _Letter[335] = "xiao";
			_IndexH[336] = 0xD0; _IndexL[336] = 0xA8; _Letter[336] = "xie";
			_IndexH[337] = 0xD0; _IndexL[337] = 0xBD; _Letter[337] = "xin";
			_IndexH[338] = 0xD0; _IndexL[338] = 0xC7; _Letter[338] = "xing";
			_IndexH[339] = 0xD0; _IndexL[339] = 0xD6; _Letter[339] = "xiong";
			_IndexH[340] = 0xD0; _IndexL[340] = 0xDD; _Letter[340] = "xiu";
			_IndexH[341] = 0xD0; _IndexL[341] = 0xE6; _Letter[341] = "xu";
			_IndexH[342] = 0xD0; _IndexL[342] = 0xF9; _Letter[342] = "xuan";
			_IndexH[343] = 0xD1; _IndexL[343] = 0xA5; _Letter[343] = "xue";
			_IndexH[344] = 0xD1; _IndexL[344] = 0xAB; _Letter[344] = "xun";
			_IndexH[345] = 0xD1; _IndexL[345] = 0xB9; _Letter[345] = "ya";
			_IndexH[346] = 0xD1; _IndexL[346] = 0xC9; _Letter[346] = "yan";
			_IndexH[347] = 0xD1; _IndexL[347] = 0xEA; _Letter[347] = "yang";
			_IndexH[348] = 0xD1; _IndexL[348] = 0xFB; _Letter[348] = "yao";
			_IndexH[349] = 0xD2; _IndexL[349] = 0xAC; _Letter[349] = "ye";
			_IndexH[350] = 0xD2; _IndexL[350] = 0xBB; _Letter[350] = "yi";
			_IndexH[351] = 0xD2; _IndexL[351] = 0xF0; _Letter[351] = "yin";
			_IndexH[352] = 0xD3; _IndexL[352] = 0xA2; _Letter[352] = "ying";
			_IndexH[353] = 0xD3; _IndexL[353] = 0xB4; _Letter[353] = "yo";
			_IndexH[354] = 0xD3; _IndexL[354] = 0xB5; _Letter[354] = "yong";
			_IndexH[355] = 0xD3; _IndexL[355] = 0xC4; _Letter[355] = "you";
			_IndexH[356] = 0xD3; _IndexL[356] = 0xD9; _Letter[356] = "yu";
			_IndexH[357] = 0xD4; _IndexL[357] = 0xA7; _Letter[357] = "yuan";
			_IndexH[358] = 0xD4; _IndexL[358] = 0xBB; _Letter[358] = "yue";
			_IndexH[359] = 0xD4; _IndexL[359] = 0xC5; _Letter[359] = "yun";
			_IndexH[360] = 0xD4; _IndexL[360] = 0xD1; _Letter[360] = "za";
			_IndexH[361] = 0xD4; _IndexL[361] = 0xD4; _Letter[361] = "zai";
			_IndexH[362] = 0xD4; _IndexL[362] = 0xDB; _Letter[362] = "zan";
			_IndexH[363] = 0xD4; _IndexL[363] = 0xDF; _Letter[363] = "zang";
			_IndexH[364] = 0xD4; _IndexL[364] = 0xE2; _Letter[364] = "zao";
			_IndexH[365] = 0xD4; _IndexL[365] = 0xF0; _Letter[365] = "ze";
			_IndexH[366] = 0xD4; _IndexL[366] = 0xF4; _Letter[366] = "zei";
			_IndexH[367] = 0xD4; _IndexL[367] = 0xF5; _Letter[367] = "zen";
			_IndexH[368] = 0xD4; _IndexL[368] = 0xF6; _Letter[368] = "zeng";
			_IndexH[369] = 0xD4; _IndexL[369] = 0xFA; _Letter[369] = "zha";
			_IndexH[370] = 0xD5; _IndexL[370] = 0xAA; _Letter[370] = "zhai";
			_IndexH[371] = 0xD5; _IndexL[371] = 0xB0; _Letter[371] = "zhan";
			_IndexH[372] = 0xD5; _IndexL[372] = 0xC1; _Letter[372] = "zhang";
			_IndexH[373] = 0xD5; _IndexL[373] = 0xD0; _Letter[373] = "zhao";
			_IndexH[374] = 0xD5; _IndexL[374] = 0xDA; _Letter[374] = "zhe";
			_IndexH[375] = 0xD5; _IndexL[375] = 0xE4; _Letter[375] = "zhen";
			_IndexH[376] = 0xD5; _IndexL[376] = 0xF4; _Letter[376] = "zheng";
			_IndexH[377] = 0xD6; _IndexL[377] = 0xA5; _Letter[377] = "zhi";
			_IndexH[378] = 0xD6; _IndexL[378] = 0xD0; _Letter[378] = "zhong";
			_IndexH[379] = 0xD6; _IndexL[379] = 0xDB; _Letter[379] = "zhou";
			_IndexH[380] = 0xD6; _IndexL[380] = 0xE9; _Letter[380] = "zhu";
			_IndexH[381] = 0xD7; _IndexL[381] = 0xA5; _Letter[381] = "zhua";
			_IndexH[382] = 0xD7; _IndexL[382] = 0xA7; _Letter[382] = "zhuai";
			_IndexH[383] = 0xD7; _IndexL[383] = 0xA8; _Letter[383] = "zhuan";
			_IndexH[384] = 0xD7; _IndexL[384] = 0xAE; _Letter[384] = "zhuang";
			_IndexH[385] = 0xD7; _IndexL[385] = 0xB5; _Letter[385] = "zhui";
			_IndexH[386] = 0xD7; _IndexL[386] = 0xBB; _Letter[386] = "zhun";
			_IndexH[387] = 0xD7; _IndexL[387] = 0xBD; _Letter[387] = "zhuo";
			_IndexH[388] = 0xD7; _IndexL[388] = 0xC8; _Letter[388] = "zi";
			_IndexH[389] = 0xD7; _IndexL[389] = 0xD7; _Letter[389] = "zong";
			_IndexH[390] = 0xD7; _IndexL[390] = 0xDE; _Letter[390] = "zou";
			_IndexH[391] = 0xD7; _IndexL[391] = 0xE2; _Letter[391] = "zu";
			_IndexH[392] = 0xD7; _IndexL[392] = 0xEA; _Letter[392] = "zuan";
			_IndexH[393] = 0xD7; _IndexL[393] = 0xEC; _Letter[393] = "zui";
			_IndexH[394] = 0xD7; _IndexL[394] = 0xF0; _Letter[394] = "zun";
			_IndexH[395] = 0xD7; _IndexL[395] = 0xF2; _Letter[395] = "zuo";
			_IndexH[396] = 0xD7; _IndexL[396] = 0xF9; _Letter[396] = string.Empty;
		}
		/// <summary>
		/// 汉字转化为拼音。
		/// </summary>
		/// <param name="character">汉字。</param>
		/// <returns>拼音。</returns>
		public static string CharacterToBopomofo(string character)
		{
			if (character != null)
			{
				List<string> letters = new List<string>();
				foreach (char word in character)
				{
					string wordString = WordToLetter(word);
					if (wordString != null)
						letters.Add(wordString[0].ToString());
				}
				return BopomofoToUpper((string.Join(string.Empty, letters.ToArray())).Trim());
			}
			return null;
		}
		/// <summary>
		/// 汉字转化为拼音缩写。
		/// </summary>
		/// <param name="character">汉字。</param>
		/// <returns>拼音缩写。</returns>
		public static string CharacterToBopomofoShort(string character)
		{
			if (character != null)
			{
				List<string> letters = new List<string>();
				foreach (char word in character)
				{
					string wordString = WordToLetter(word);
					if (wordString != null)
						letters.Add(wordString[0].ToString());
				}
				return (string.Join(string.Empty, letters.ToArray())).Trim();
			}
			return null;
		}
		/// <summary>
		/// 汉字转化为拼音。
		/// </summary>
		/// <param name="word">汉字。</param>
		/// <returns>拼音。</returns>
		public static string WordToLetter(char word)
		{
			byte[] indices = _Encoding.GetBytes(word.ToString());
			if (indices.Length == 2)
			{
				int index = indices[0] * 256 + indices[1];
				for (int i = 0; i < 396; i++)
				{
					if (index >= _IndexH[i] * 256 + _IndexL[i] && index < _IndexH[i + 1] * 256 + _IndexL[i + 1])
					{
						return _Letter[i];
					}
				}
			}
			return null;
		}
		/// <summary>
		/// 拼音首字变大写。
		/// </summary>
		/// <param name="bopomofo">拼音。</param>
		/// <returns>拼音。</returns>
		public static string BopomofoToUpper(string bopomofo)
		{
			if (bopomofo == null)
				return null;
			else
				return string.Concat(bopomofo.Substring(0, 1).ToUpper(), bopomofo.Substring(1));
		}
	}
}
