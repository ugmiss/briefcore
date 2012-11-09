﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Utility.Hanzi
{
    public class CnNameFactory
    {
        public CnNameFactory()
        {
        }
        const string FirstName = @"
赵 钱 孙 李 周 吴 郑 王 
冯 陈 褚 卫 蒋 沈 韩 杨 
朱 秦 尤 许 何 吕 施 张 
孔 曹 严 华 金 魏 陶 姜 
戚 谢 邹 喻 柏 水 窦 章 
云 苏 潘 葛 奚 范 彭 郎 
鲁 韦 昌 马 苗 凤 花 方 
俞 任 袁 柳 酆 鲍 史 唐 
费 廉 岑 薛 雷 贺 倪 汤 
滕 殷 罗 毕 郝 邬 安 常 
乐 于 时 傅 皮 卞 齐 康 
伍 余 元 卜 顾 孟 平 黄 
和 穆 萧 尹 姚 邵 湛 汪 
祁 毛 禹 狄 米 贝 明 臧 
计 伏 成 戴 谈 宋 茅 庞 
熊 纪 舒 屈 项 祝 董 梁 
杜 阮 蓝 闵 席 季 麻 强 
贾 路 娄 危 江 童 颜 郭 
梅 盛 林 刁 钟 徐 邱 骆 
高 夏 蔡 田 樊 胡 凌 霍 
虞 万 支 柯 昝 管 卢 莫 
经 房 裘 缪 干 解 应 宗 
丁 宣 贲 邓 郁 单 杭 洪 
包 诸 左 石 崔 吉 钮 龚 
程 嵇 邢 滑 裴 陆 荣 翁 
荀 羊 於 惠 甄 曲 家 封 
芮 羿 储 靳 汲 邴 糜 松 
井 段 富 巫 乌 焦 巴 弓 
牧 隗 山 谷 车 侯 宓 蓬 
全 郗 班 仰 秋 仲 伊 宫 
宁 仇 栾 暴 甘 钭 厉 戎 
祖 武 符 刘 景 詹 束 龙 
叶 幸 司 韶 郜 黎 蓟 薄 
印 宿 白 怀 蒲 台 从 鄂 
索 咸 籍 赖 卓 蔺 屠 蒙 
池 乔 阴 郁 胥 能 苍 双 
闻 莘 党 翟 谭 贡 劳 逄 
姬 申 扶 堵 冉 宰 郦 雍 
郤 璩 桑 桂 濮 牛 寿 通 
边 扈 燕 冀 郏 浦 尚 农 
温 别 庄 晏 柴 瞿 阎 充 
慕 连 茹 习 宦 艾 鱼 容 
向 古 易 慎 戈 廖 庚 终 
暨 居 衡 步 都 耿 满 弘 
匡 国 文 寇 广 禄 阙 东 
殴 殳 沃 利 蔚 越 夔 隆 
师 巩 厍 聂 晁 勾 敖 融 
冷 訾 辛 阚 那 简 饶 空 
曾 毋 沙 乜 养 鞠 须 丰 
巢 关 蒯 相 查 后 荆 红 
游 竺 权 逯 盖 益 桓 公 
万俟 司马 上官 欧阳 
夏侯 诸葛 闻人 东方 
赫连 皇甫 尉迟 公羊 
澹台 公冶 宗政 濮阳 
淳于 单于 太叔 申屠 
公孙 仲孙 轩辕 令狐 
钟离 宇文 长孙 慕容 
鲜于 闾丘 司徒 司空 
亓官 司寇 仉 督 子车 
颛孙 端木 巫马 公西 
漆雕 乐正 壤驷 公良 
拓拔 夹谷 宰父 谷粱 
晋 楚 闫 法 汝 鄢 涂 钦 
段干 百里 东郭 南门 
呼延 归 海 羊舌 微生 
岳 帅 缑 亢 况 后 有 琴 
梁丘 左丘 东门 西门 
商 牟 佘 佴 伯 赏 南宫 
墨 哈 谯 笪 年 爱 阳 佟 
第五 言 福";
        const string boy = @"
安邦 安福 安歌 安国 安和 安康 安澜 安民 安宁 安平 安然 安顺 
安翔 安晏 安宜 安怡 安易 安志 昂然 昂雄 
宾白 宾鸿 宾实 彬彬 彬炳 彬郁 斌斌 斌蔚 滨海 波光 波鸿 波峻 
波涛 博瀚 博超 博达 博厚 博简 博明 博容 博赡 博涉 博实 博涛 
博文 博学 博雅 博延 博艺 博易 博裕 博远 
才捷 才良 才艺 才英 才哲 才俊 成和 成弘 成化 成济 成礼 成龙 
成仁 成双 成天 成文 成业 成益 成荫 成周 承安 承弼 承德 承恩 
承福 承基 承教 承平 承嗣 承天 承望 承宣 承颜 承业 承悦 承允 
承运 承载 承泽 承志 
德本 德海 德厚 德华 德辉 德惠 德容 德润 德寿 德水 德馨 德曜 
德业 德义 德庸 德佑 德宇 德元 德运 德泽 德明 
飞昂 飞白 飞飙 飞掣 飞尘 飞沉 飞驰 飞光 飞翰 飞航 飞翮 飞鸿 
飞虎 飞捷 飞龙 飞鸾 飞鸣 飞鹏 飞扬 飞文 飞翔 飞星 飞翼 飞英 
飞宇 飞羽 飞雨 飞语 飞跃 飞章 飞舟 风华 丰茂 丰羽 
刚豪 刚洁 刚捷 刚毅 高昂 高岑 高畅 高超 高驰 高达 高澹 高飞 
高芬 高峯 高峰 高歌 高格 高寒 高翰 高杰 高洁 高峻 高朗 高丽 
高邈 高旻 高明 高爽 高兴 高轩 高雅 高扬 高阳 高义 高谊 高逸 
高懿 高原 高远 高韵 高卓 光赫 光华 光辉 光济 光霁 光亮 光临 
光明 光启 光熙 光耀 光誉 光远 国安 国兴 国源 冠宇 冠玉 
晗昱 晗日 涵畅 涵涤 涵亮 涵忍 涵容 涵润 涵涵 涵煦 涵蓄 涵衍 
涵意 涵映 涵育 翰采 翰池 翰飞 翰海 翰翮 翰林 翰墨 翰学 翰音 
瀚玥 翰藻 瀚海 瀚漠 昊苍 昊昊 昊空 昊乾 昊穹 昊然 昊然 昊天 
昊焱 昊英 浩波 浩博 浩初 浩大 浩宕 浩荡 浩歌 浩广 浩涆 浩瀚 
浩浩 浩慨 浩旷 浩阔 浩漫 浩淼 浩渺 浩邈 浩气 浩然 浩穰 浩壤 
浩思 浩言 皓轩 和蔼 和安 和璧 和昶 和畅 和风 和歌 和光 和平 
和洽 和惬 和顺 和硕 和颂 和泰 和悌 和通 和同 和煦 和雅 和宜 
和怡 和玉 和裕 和豫 和悦 和韵 和泽 和正 和志 鹤轩 弘博 弘大 
弘方 弘光 弘和 弘厚 弘化 弘济 弘阔 弘亮 弘量 弘深 弘盛 弘图 
弘伟 弘文 弘新 弘雅 弘扬 弘业 弘义 弘益 弘毅 弘懿 弘致 弘壮 
宏伯 宏博 宏才 宏畅 宏达 宏大 宏放 宏富 宏峻 宏浚 宏恺 宏旷 
宏阔 宏朗 宏茂 宏邈 宏儒 宏深 宏胜 宏盛 宏爽 宏硕 宏伟 宏扬 
宏义 宏逸 宏毅 宏远 宏壮 鸿宝 鸿波 鸿博 鸿才 鸿彩 鸿畅 鸿畴 
鸿达 鸿德 鸿飞 鸿风 鸿福 鸿光 鸿晖 鸿朗 鸿文 鸿熙 鸿羲 鸿禧 
鸿信 鸿轩 鸿煊 鸿煊 鸿雪 鸿羽 鸿远 鸿云 鸿运 鸿哲 鸿祯 鸿振 
鸿志 鸿卓 华奥 华采 华彩 华灿 华藏 华池 华翰 华皓 华晖 华辉 
华茂 华美 华清 华荣 华容 
嘉赐 嘉德 嘉福 嘉良 嘉茂 嘉木 嘉慕 嘉纳 嘉年 嘉平 嘉庆 嘉荣 
嘉容 嘉瑞 嘉胜 嘉石 嘉实 嘉树 嘉澍 嘉熙 嘉禧 嘉祥 嘉歆 嘉许 
嘉勋 嘉言 嘉谊 嘉懿 嘉颖 嘉佑 嘉玉 嘉誉 嘉悦 嘉运 嘉泽 嘉珍 
嘉祯 嘉志 嘉致 坚白 坚壁 坚秉 坚成 坚诚 建安 建白 建柏 建本 
建弼 建德 建华 建明 建茗 建木 建树 建同 建修 建业 建义 建元 
建章 建中 健柏 金鑫 锦程 瑾瑜 晋鹏 经赋 经亘 经国 经略 经纶 
经纬 经武 经业 经义 经艺 景澄 景福 景焕 景辉 景辉 景龙 景明 
景山 景胜 景铄 景天 景同 景曜 靖琪 君昊 君浩 俊艾 俊拔 俊弼 
俊才 俊材 俊驰 俊楚 俊达 俊德 俊发 俊风 俊豪 俊健 俊杰 俊捷 
俊郎 俊力 俊良 俊迈 俊茂 俊美 俊民 俊名 俊明 俊楠 俊能 俊人 
俊爽 俊悟 俊晤 俊侠 俊贤 俊雄 俊雅 俊彦 俊逸 俊英 俊友 俊语 
俊誉 俊远 俊哲 俊喆 俊智 峻熙 季萌 季同 
开畅 开诚 开宇 开济 开霁 开朗 凯安 凯唱 凯定 凯风 凯复 凯歌 
凯捷 凯凯 凯康 凯乐 凯旋 凯泽 恺歌 恺乐 康安 康伯 康成 康德 
康复 康健 康乐 康宁 康平 康胜 康盛 康时 康适 康顺 康泰 康裕 
乐安 乐邦 乐成 乐池 乐和 乐家 乐康 乐人 乐容 乐山 乐生 乐圣 
乐水 乐天 乐童 乐贤 乐心 乐欣 乐逸 乐意 乐音 乐咏 乐游 乐语 
乐悦 乐湛 乐章 乐正 乐志 黎昕 黎明 力夫 力强 力勤 力行 力学 
力言 立诚 立果 立人 立辉 立轩 立群 良奥 良弼 良才 良材 良策 
良畴 良工 良翰 良吉 良骥 良俊 良骏 良朋 良平 良哲 理群 理全 
茂才 茂材 茂德 茂典 茂实 茂学 茂勋 茂彦 敏博 敏才 敏达 敏叡 
敏学 敏智 明诚 明达 明德 明辉 明杰 明俊 明朗 明亮 明旭 明煦 
明轩 明远 明哲 明喆 明知 明志 明智 明珠 
朋兴 朋义 彭勃 彭薄 彭湃 彭彭 彭魄 彭越 彭泽 彭祖 鹏程 鹏池 
鹏飞 鹏赋 鹏海 鹏鲸 鹏举 鹏鹍 鹏鲲 鹏涛 鹏天 鹏翼 鹏云 鹏运 
濮存 溥心 璞玉 璞瑜 浦和 浦泽 
奇略 奇迈 奇胜 奇水 奇思 奇邃 奇伟 奇玮 奇文 奇希 奇逸 奇正 
奇志 奇致 祺福 祺然 祺祥 祺瑞 琪睿 庆生 
荣轩 锐达 锐锋 锐翰 锐进 锐精 锐立 锐利 锐思 锐逸 锐意 锐藻 
锐泽 锐阵 锐志 锐智 睿博 睿才 睿诚 睿慈 睿聪 睿达 睿德 睿范 
睿广 睿好 睿明 睿识 睿思 
绍辉 绍钧 绍祺 绍元 升荣 圣杰 晟睿 思聪 思淼 思源 思远 思博 
斯年 斯伯 同济
泰初 泰和 泰河 泰鸿 泰华 泰宁 泰平 泰清 泰然 天材 天成 天赋 
天干 天罡 天工 天翰 天和 天华 天骄 天空 天禄 天路 天瑞 天睿 
天逸 天佑 天宇 天元 天韵 天泽 天纵 同方 同甫 同光 同和 同化 
巍昂 巍然 巍奕 伟博 伟毅 伟才 伟诚 伟茂 伟懋 伟祺 伟彦 伟晔 
伟泽 伟兆 伟志 温纶 温茂 温书 温韦 温文 温瑜 文柏 文昌 文成 
文德 文栋 文赋 文光 文翰 文虹 文华 文康 文乐 文林 文敏 文瑞 
文山 文石 文星 文轩 文宣 文彦 文曜 文耀 文斌 文彬 文滨 
向晨 向笛 向文 向明 向荣 向阳 翔宇 翔飞 项禹 项明 晓博 心水 
心思 心远 欣德 欣嘉 欣可 欣然 欣荣 欣怡 欣怿 欣悦 新翰 新霁 
新觉 新立 新荣 新知 信鸿 信厚 信鸥 信然 信瑞 兴安 兴邦 兴昌 
兴朝 兴德 兴发 兴国 兴怀 兴平 兴庆 兴生 兴思 兴腾 兴旺 兴为 
兴文 兴贤 兴修 兴学 兴言 兴业 兴运 星波 星辰 星驰 星光 星海 
星汉 星河 星华 星晖 星火 星剑 星津 星阑 星纬 星文 星宇 星雨 
星渊 星洲 修诚 修德 修杰 修洁 修谨 修筠 修明 修能 修平 修齐 
修然 修为 修伟 修文 修雅 修永 修远 修真 修竹 修贤 旭尧 炫明 
学博 学海 学林 学民 学名 学文 学义 学真 雪松 雪峰 雪风 
雅昶 雅畅 雅达 雅惠 雅健 雅珺 雅逸 雅懿 雅志 炎彬 阳飙 阳飇 
阳冰 阳波 阳伯 阳成 阳德 阳华 阳晖 阳辉 阳嘉 阳平 阳秋 阳荣 
阳舒 阳朔 阳文 阳曦 阳夏 阳旭 阳煦 阳炎 阳焱 阳曜 阳羽 阳云 
阳泽 阳州 烨赫 烨华 烨磊 烨霖 烨然 烨烁 烨伟 烨烨 烨熠 烨煜 
毅然 逸仙 逸明 逸春 宜春 宜民 宜年 宜然 宜人 宜修 意远 意蕴 
意致 意智 熠彤 懿轩 英飙 英博 英才 英达 英发 英范 英光 英豪 
英华 英杰 英朗 英锐 英睿 英叡 英韶 英卫 英武 英悟 英勋 英彦 
英耀 英奕 英逸 英毅 英哲 英喆 英卓 英资 英纵 永怡 永春 永安 
永昌 永长 永丰 永福 永嘉 永康 永年 永宁 永寿 永思 永望 永新 
永言 永逸 永元 永贞 咏德 咏歌 咏思 咏志 勇男 勇军 勇捷 勇锐 
勇毅 宇达 宇航 宇寰 宇文 宇荫 雨伯 雨华 雨石 雨信 雨星 雨泽 
玉宸 玉成 玉龙 玉泉 玉山 玉石 玉书 玉树 玉堂 玉轩 玉宇 玉韵 
玉泽 煜祺 元白 元德 元化 元基 元嘉 元甲 元驹 元凯 元恺 元魁 
元良 元亮 元龙 元明 元青 元思 元纬 元武 元勋 元正 元忠 元洲 
远航 苑博 苑杰 越彬 蕴涵 蕴和 蕴藉 
展鹏 哲瀚 哲茂 哲圣 哲彦 振海 振国 正诚 正初 正德 正浩 正豪 
正平 正奇 正青 正卿 正文 正祥 正信 正雅 正阳 正业 正谊 正真 
正志 志诚 志新 志勇 志明 志国 志强 志尚 志专 志文 志行 志学 
志业 志义 志用 志泽 致远 智明 智鑫 智勇 智敏 智志 智渊 子安 
子晋 子民 子明 子默 子墨 子平 子琪 子石 子实 子真 子濯 子昂 
子轩 子瑜 自明 自强 作人 自怡 自珍 曾琪 泽宇 泽语
";
        const string girls = @"
安安 安吉 安静 安娜 安妮 安琪 安然 安娴 安祯 荌荌 奥婷 奥维 奥雅
北辰 北嘉 北晶 贝莉 贝丽 琲瓃 蓓蕾 碧菡 碧琳 碧莹 碧玉 冰冰 冰枫 冰洁 
冰蓝 冰心 冰彦 冰莹 博丽 博敏 博雅 布凡 布侬 布欣 布衣 
偲偲 采莲 采薇 采文 采萱 彩静 彩萱 彩妍 灿灿 婵娟 畅畅 畅然
唱月 朝旭 朝雨 琛丽 琛瑞 晨曦 晨旭 初然 初阳 楚楚 楚洁 楚云
春芳 春华 春娇 春兰 春岚 春梅 春桃 春晓 春雪 春燕 春英 春雨
淳静 淳美 淳雅 慈心 聪慧 聪睿 翠茵 
黛娥 丹丹 丹红 丹彤 丹溪 笛韵 典丽 典雅 蝶梦 丁辰 丁兰 冬梅
端静 端丽 端敏 端雅 端懿 多思 朵儿 
婀娜 恩霈 尔雅 璠瑜 方方 方雅 方仪 芳蔼 芳春 芳芳 芳菲 芳馥 
芳华 芳蕙 芳洁 芳林 芳苓 芳荃 芳蕤 芳润 芳馨 芳懿 芳茵 芳泽 
芳洲 飞雪 飞燕 菲菲 霏霏 斐斐 芬菲 芬芬 芬馥 丰熙 丰雅 芙蓉 
馥芬 
甘雨 甘泽 高洁 歌阑 歌云 歌韵 格菲 格格 葛菲 古兰 古香 古韵 
谷雪 谷玉 瑰玮 桂帆 桂枫 桂华 桂月 桂芝 
海儿 海女 含娇 含景 含文 含香 含秀 含玉 晗玥 涵涵 涵菡 涵韵 
寒梅 菡梅 好洁 好慕 浩岚 浩丽 皓洁 皓月 合乐 合美 合瑞 和璧 
和静 和美 和暖 和平 和悌 和煦 和暄 和雅 和怡 和玉 和豫 和悦 
河灵 荷珠 荷紫 赫然 鹤梦 姮娥 弘丽 弘雅 弘懿 红豆 红旭 红叶 
闳丽 虹星 虹英 虹颖 虹影 虹雨 虹玉 华采 华楚 华乐 华婉 华月 
华芝 怀慕 怀思 怀玉 欢欣 欢悦 会雯 会欣 彗云 惠丽 惠美 惠然 
惠心 慧婕 慧君 慧丽 慧美 慧心 慧秀 慧雅 慧艳 慧英 慧颖 慧语 
慧月 慧云 蕙兰 蕙若 
吉帆 吉玟 吉敏 吉欣 吉星 吉玉 吉月 季雅 霁芸 佳惠 佳美 佳思 
佳文 佳妍 佳悦 家美 家欣 家馨 嘉宝 嘉惠 嘉丽 嘉美 嘉禾 嘉淑 
嘉歆 嘉言 嘉怡 嘉懿 嘉音 嘉颖 嘉玉 嘉月 嘉悦 嘉云 江雪 姣姣 
姣丽 姣妍 娇洁 娇然 皎洁 皎月 杰秀 洁静 洁雅 洁玉 今歌 今瑶 
今雨 金玉 金枝 津童 锦凡 锦诗 锦文 锦欣 瑾瑶 菁菁 菁英 晶辉 
晶晶 晶灵 晶滢 靓影 静安 静枫 静涵 静和 静慧 静曼 静美 静淑 
静恬 静婉 静娴 静秀 静雅 静逸 静云 菊华 菊月 娟娟 娟丽 娟秀 
娟妍 绢子 隽洁 隽美 隽巧 隽雅 君洁 君丽 君雅 筠溪 筠心 筠竹 
俊慧 俊雅 珺俐 珺琦 珺琪 珺娅 
可可 可儿 可佳 可嘉 可心 琨瑶 琨瑜 兰芳 兰蕙 兰梦 兰娜 兰若 
兰英 兰月 兰泽 兰芝 岚翠 岚风 岚岚 蓝尹 朗丽 朗宁 朗然 乐然 
乐容 乐心 乐欣 乐怡 乐悦 莉莉 丽芳 丽华 丽佳 丽姝 丽思 丽文 
丽雅 丽玉 丽泽 丽珠 林帆 林楠 琳芳 琳怡 琳瑜 伶俐 伶伶 灵卉 
灵慧 灵秀 灵萱 灵雨 灵韵 玲琅 玲琳 玲玲 玲珑 玲然 凌波 凌春 
凌晓 凌雪 铃语 菱凡 菱华 令慧 令美 流惠 流丽 流如 流婉 流逸 
柳思 珑玲 芦雪 罗绮 洛妃 洛灵 
玛丽 麦冬 曼丽 曼蔓 曼妮 曼青 曼容 曼婉 曼文 曼吟 曼语 曼云 
曼珠 嫚儿 蔓菁 蔓蔓 梅风 梅红 梅花 梅梅 梅青 梅雪 梅英 美偲 
美华 美丽 美曼 美如 萌阳 蒙雨 孟乐 孟夏 孟阳 梦凡 梦菲 梦菡 
梦华 梦兰 梦露 梦琪 梦秋 梦丝 梦桐 梦影 梦雨 梦月 梦云 梦泽 
梦竹 米琪 米雪 密如 密思 淼淼 妙婧 妙晴 妙思 妙颜 妙意 妙音 
妙珍 玟丽 玟玉 珉瑶 闵雨 敏慧 敏丽 敏叡 敏思 名姝 明煦 明艳 
鸣晨 鸣玉 茗雪 茉莉 木兰 牧歌 慕梅 慕诗 慕思 慕悦 暮雨 暮芸 
娜兰 娜娜 乃心 乃欣 囡囡 楠楠 妮娜 妮子 霓云 旎旎 念念 宁乐 
凝洁 凝静 凝然 凝思 凝心 凝雪 凝雨 凝远 妞妞 浓绮 暖暖 暖姝 
暖梦 
盼盼 沛若 沛珊 沛文 佩兰 佩杉 佩玉 佩珍 芃芃 彭丹 嫔然 品韵 
平和 平惠 平乐 平良 平宁 平婉 平晓 平心 平雅 平莹 萍雅 萍韵 
璞玉 
齐敏 齐心 其雨 奇思 奇文 奇颖 颀秀 琦巧 琦珍 琪华 启颜 绮怀 
绮丽 绮梅 绮美 绮梦 绮思 绮文 绮艳 绮玉 绮云 千秋 千叶 芊丽 
芊芊 茜茜 倩丽 倩美 倩秀 倩语 俏丽 俏美 琴心 琴轩 琴雪 琴音 
琴韵 卿月 卿云 清昶 清芬 清涵 清华 清晖 清霁 清嘉 清宁 清奇 
清绮 清秋 清润 清淑 清舒 清婉 清心 清馨 清雅 清妍 清一 清漪 
清怡 清逸 清懿 清莹 清悦 清韵 清卓 情文 情韵 晴波 晴虹 晴画 
晴岚 晴丽 晴美 晴曦 晴霞 晴雪 琼芳 琼华 琼岚 琼诗 琼思 琼怡 
琼音 琼英 秋芳 秋华 秋露 秋荣 秋彤 秋阳 秋英 秋颖 秋玉 秋月 
秋芸 曲静 曲文 
冉冉 苒苒 荏苒 任真 溶溶 蓉城 蓉蓉 融雪 柔怀 柔惠 柔洁 柔谨 
柔静 柔丽 柔蔓 柔妙 柔淑 柔婉 柔煦 柔绚 柔雅 如冰 如风 如心 
如馨 如雪 如仪 如意 如云 茹薇 茹雪 茹云 濡霈 蕊珠 芮安 芮波 
芮欢 芮佳 芮静 芮澜 芮丽 芮美 芮欣 芮雅 芮优 芮悦 瑞彩 瑞锦 
瑞灵 瑞绣 瑞云 瑞芝 睿敏 睿思 睿彤 睿文 睿哲 睿姿 润丽 若芳 
若华 若兰 若淑 若彤 若英 若云 
莎莉 莎莎 三春 三姗 三诗 森莉 森丽 沙羽 沙雨 杉月 姗姗 珊珊 
善芳 善和 善静 善思 韶华 韶丽 韶美 韶敏 韶容 韶阳 韶仪 邵美 
沈靖 沈静 沈然 沈思 沈雅 诗怀 诗兰 诗蕾 诗柳 诗蕊 诗文 施然 
施诗 世敏 世英 世韵 书慧 书桃 书文 书萱 书仪 书艺 书意 书语 
书云 抒怀 姝好 姝惠 姝丽 姝美 姝艳 淑华 淑惠 淑慧 淑静 淑兰 
淑穆 淑然 淑婉 淑贤 淑雅 淑懿 淑哲 淑贞 舒畅 舒方 舒怀 舒兰 
舒荣 舒扬 舒云 帅红 双文 双玉 水晶 水悦 水芸 顺慈 顺美 丝柳 
丝萝 丝娜 丝琦 丝琪 丝祺 丝微 丝雨 司辰 司晨 思嫒 思宸 思聪 
思迪 思恩 思凡 思涵 思慧 思佳 思嘉 思洁 思莲 思琳 思美 思萌 
思敏 思娜 思楠 思琪 思若 思思 思彤 思溪 思雅 思怡 思义 思懿 
思茵 思莹 思雨 思语 思云 斯琪 斯乔 斯斯 斯文 斯雅 松雪 松雨 
松月 素华 素怀 素洁 素昕 素欣 
棠华 洮洮 桃雨 陶宁 陶然 陶宜 天恩 天慧 天骄 天籁 天蓝 天睿 
天心 天欣 天音 天媛 天悦 天韵 田然 田田 恬畅 恬静 恬美 恬谧 
恬默 恬然 恬欣 恬雅 恬悦 甜恬 湉湉 听然 听云 婷美 婷然 婷婷 
婷秀 婷玉 彤蕊 彤彤 彤雯 彤霞 彤云 桐华 桐欣 童彤 童童 童欣 
宛儿 宛畅 宛曼 宛妙 莞尔 莞然 婉慧 婉静 婉丽 婉娜 婉清 婉然 
婉容 婉柔 婉淑 婉秀 婉仪 婉奕 菀柳 菀菀 琬凝 琬琰 望慕 望舒 
望雅 微澜 微婉 微熹 微月 薇歌 韦曲 韦柔 韦茹 苇然 玮奇 玮琪 
玮艺 未央 蔚然 文惠 文静 文君 文丽 文敏 文墨 文姝 文思 文心 
文瑶 文漪 文茵 雯华 雯丽 问筠 问梅 问萍 梧桐 
西华 希月 希恩 希慕 希蓉 希彤 惜文 惜雪 惜玉 熙华 熙柔 熙熙 
熙阳 熙怡 喜儿 喜悦 霞飞 霞雰 霞辉 霞绮 霞姝 霞文 夏菡 夏兰 
夏岚 夏青 夏彤 夏萱 夏璇 夏雪 夏月 仙仪 仙媛 仙韵 闲华 闲静 
闲丽 贤惠 贤淑 咸英 娴静 娴淑 娴婉 娴雅 羡丽 献仪 献玉 香洁 
香梅 香馨 香雪 湘君 湘灵 湘云 向晨 宵晨 宵雨 宵月 萧曼 萧玉 
箫笛 箫吟 小春 小枫 小谷 小楠 小琴 小雯 小星 小瑜 小雨 晓畅 
晓凡 晓枫 晓慧 晓兰 晓莉 晓曼 晓楠 晓彤 晓桐 晓星 晓燕 笑寒 
笑雯 笑笑 笑妍 心慈 心菱 心诺 心香 心宜 心怡 心语 心远 忻畅 
忻欢 忻乐 忻慕 忻然 忻忻 忻愉 昕昕 欣彩 欣畅 欣合 欣嘉 欣可 
欣美 欣然 欣彤 欣笑 欣欣 欣艳 欣怡 欣愉 欣悦 欣跃 莘莘 新洁 
新蕾 新林 新梅 新美 新苗 新荣 新文 新雪 新雅 新颖 新雨 新语 
新月 歆美 歆然 馨兰 馨荣 馨蓉 馨香 馨欣 杏儿 修洁 修美 修敏 
秀华 秀慧 秀杰 秀洁 秀娟 秀隽 秀筠 秀兰 秀丽 秀曼 秀梅 秀美 
秀媚 秀敏 秀妮 秀婉 秀雅 秀艳 秀逸 秀英 秀颖 秀媛 秀越 秀竹 
绣文 绣梓 琇芳 琇芬 琇晶 琇莹 琇云 轩秀 萱彤 暄和 暄美 暄妍 
玄静 玄穆 玄清 玄素 玄雅 璇玑 璇娟 璇珠 璇子 雪冰 雪儿 雪帆 
雪枫 雪卉 雪翎 雪柳 雪曼 雪漫 雪萍 雪晴 雪艳 雪羽 寻春 寻芳 
雅爱 雅安 雅唱 雅丹 雅凡 雅歌 雅惠 雅洁 雅静 雅隽 雅可 雅丽 
雅美 雅楠 雅宁 雅萍 雅琴 雅容 雅柔 雅蕊 雅韶 雅诗 雅素 雅彤 
雅娴 雅秀 雅艳 雅逸 雅懿 雅云 雅韵 雅致 娅芳 娅静 娅玟 娅楠 
娅思 娅童 娅欣 嫣然 妍芳 妍歌 妍和 妍丽 妍雅 妍妍 芫华 言文 
言心 俨雅 琰琬 彦红 彦慧 彦珺 彦灵 彦露 彦杉 彦芝 晏静 晏然 
晏如 艳芳 艳卉 艳蕙 燕桦 燕珺 燕岚 燕楠 燕妮 燕婉 燕舞 燕子 
阳霁 阳兰 阳曦 阳阳 杨柳 洋然 洋洋 漾漾 瑶岑 瑶瑾 野雪 野云 
叶春 叶丹 叶帆 叶芳 叶飞 叶丰 叶吉 叶嘉 叶农 叶彤 叶舞 叶欣 
晔晔 一凡 一禾 一嘉 一瑾 一南 一雯 一璇 依白 依波 依美 依楠 
依秋 依然 依珊 依童 依心 依云 仪芳 仪文 宜春 宜嘉 宜楠 宜然 
宜欣 怡畅 怡和 怡嘉 怡乐 怡璐 怡木 怡宁 怡然 怡月 怡悦 颐和 
颐然 颐真 以晴 以蕊 以彤 以欣 以轩 佁然 倚云 忆安 忆梅 忆敏 
忆秋 忆然 忆彤 忆雪 忆远 怿悦 奕叶 奕奕 轶丽 逸丽 逸美 逸思 
逸馨 逸秀 逸雅 逸云 逸致 茵茵 音华 音景 音仪 音悦 音韵 吟怀 
银河 银柳 银瑶 饮香 饮月 胤文 胤雅 英华 英慧 英楠 英秀 英媛 
莺莺 莺语 莺韵 瑛琭 瑛瑶 樱花 璎玑 迎梅 迎秋 盈盈 莹白 莹华 
莹洁 莹然 莹琇 莹莹 莹玉 萦怀 萦思 萦心 滢渟 滢滢 颖初 颖慧 
颖然 颖馨 颖秀 颖颖 映波 映寒 映秋 映雪 雍恬 雍雅 优乐 优扬 
优悠 优瑗 优悦 攸然 悠柔 悠素 悠婉 悠馨 悠雅 悠奕 悠逸 幼安 
幼仪 幼怡 余馥 余妍 愉婉 愉心 瑜蓓 瑜璟 瑜敏 瑜然 瑜英 羽彤 
雨筠 雨彤 雨燕 雨竹 语冰 语林 语诗 语彤 语心 语燕 玉琲 玉华 
玉环 玉瑾 玉珂 玉兰 玉轩 玉怡 玉英 元英 爰美 爰爰 源源 远悦 
媛女 月桂 月华 月朗 月灵 月明 月杉 月桃 月天 月怡 月悦 悦爱 
悦畅 悦和 悦恺 悦可 悦来 悦乐 悦人 悦喜 悦心 悦欣 悦宜 悦怡 
悦远 悦媛 云淡 云飞 云岚 云露 云梦 云韶 云水 云亭 云蔚 云溪 
云霞 云心 云英 芸芸 韫素 韫玉 韵流 韵梅 韵宁 韵磬 韵诗 蕴涵 
蕴和 蕴美 蕴秀 
赞怡 赞悦 泽恩 泽惠 湛恩 湛芳 湛静 昭懿 昭昭 哲丽 哲美 哲思 
哲妍 贞芳 贞静 贞婉 贞怡 贞韵 珍丽 珍瑞 真洁 真如 真茹 真一 
真仪 正平 正清 正思 正雅 之桃 芝兰 芝英 芝宇 知慧 知睿 芷兰 
芷琪 芷若 芷文 致欣 致萱 智美 智敏 仲舒 珠佩 珠星 珠轩 珠雨 
珠玉 竹筱 竹萱 竹雨 竹月 竹悦 竹韵 庄静 庄丽 庄雅 卓然 卓逸 
子爱 子丹 子帆 子凡 子菡 子怀 子惠 子蕙 子琳 子美 子楠 子宁 
子舒 子童 子薇 子欣 子萱 子璇 子怡 子亦 子悦 子珍 梓蓓 梓涵 
梓洁 梓菱 梓露 梓璐 梓美 梓敏 梓楠 梓倩 梓柔 梓珊 梓舒 梓婷 
梓彤 梓童 梓琬 梓欣 梓馨 梓萱 梓瑶 梓莹 梓颖 梓榆 梓玥 梓云 
紫蕙 紫琼 紫杉 紫桐 紫薇 紫玉 
";
        public string GetNewName(out bool IsMale)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode()); 
            if (r.Next(2) > 0)
            {
                IsMale = true;
                return GetBoyName();
            }
            else
            {
                IsMale = false;
                return GetGirlName();
            }
        }
        static string[] boynames = boy.Split(" \n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        static string[] fs = FirstName.Split(" \n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        static string[] girlsnames = girls.Split(" \n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        public string GetBoyName()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode()); 
            string name = fs[r.Next(fs.Length)] + boynames[r.Next(boynames.Length)];
            return name;
        }
        public string GetGirlName()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode()); 
            string name = fs[r.Next(fs.Length)] + girlsnames[r.Next(girlsnames.Length)];
            return name;
        }
    }
}
