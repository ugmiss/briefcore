## 棋盘 ##
棋盘5\*5方格，25个位置
## 方式 ##
黑先白后轮流下棋
按积分，结算时，分数相互抵扣，按分数摘对方子，
先手先摘先走，后摘后走。
分数相等，各摘1子，轮流走棋子，一步一格，只能横竖走，成分继续摘子，历史成分不能重复摘子。
### 积分规则 ###
通天：5连的斜线，5分
五虎：5连的横竖，4分
四斜：顶点在边上的4连斜线，3分
三斜：顶点在边上的3连斜线，2分
井：接连并成正方形的4子，1分
### 技巧 ###
通常开局
先手抢三斜位置和中心位置
之后四斜位置上厮杀
五虎通天难度较大，通过运子成斜时关联成井
一般性领先1子就很难下了
基本上都是平分互摘1子。
### 算法 ###
封，防守封锁对手得分位
抢，抢分位
对，对等位
线段树，对落子顺序形成节点树，不断延伸，计算树枝的评分，并回归树上级，加分，最终统计最高分位点