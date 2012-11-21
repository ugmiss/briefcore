/**
 * 初始化 Cookie
 */
function initCookie(){
	try{
		if(getCounterCookie('tmg_utma') == null){		// 如果 UV 不存在
			addCookie('tmg_utma',getUID(),2*12*30*24*60);
		}
		if(getCounterCookie('tmg_utmb') != null){		// 30分钟内活动
			setCookieTime('tmg_utmb',30);
		}else{				  				// 30分钟失效后
			addCookie('tmg_utmb',getUID(),30); // 更新 UID
		}
	}catch(error){}
}

/**
 *  发送请求
 */
var isSended = '-1'; // 判断是否已改送
function sentParamter(AID,TYPE,parm1,parm2,parm3){
	if(!AID) AID = -1;
	if(!TYPE) TYPE = -1;
	if(!parm1) parm1 = -1;
	if(!parm2) parm2 = -1;
	if(!parm3) parm3 = -1;
	
	if(isSended == '-1'){
		document.write("<img src='http://test.counter.yesky.com/counter.shtml?UV="
			+ getCounterCookie("tmg_utma")+"&amp;VS="
			+ getCounterCookie("tmg_utmb")+"&amp;refer="
			+ encodeURIComponent(document.referrer)+"&amp;rand="
			+ new Date().getTime()+"&amp;cur="
			+ encodeURIComponent(document.URL)+"&amp;language="
			+ getSystemLanguage()+"&amp;system="
			+ getSystem()+"&amp;browser="
			+ getBrowser()+"&amp;flashVesion="
			+ getFlashVesion()+"&amp;resolution="
			+ getResolution()+"&amp;colorDepath="
			+ getColorDepth()+"&amp;AID="
			+ AID+"&amp;TYPE="
			+ TYPE+"&amp;PARM1="
			+ parm1+"&amp;PARM2="
			+ parm2+"&amp;PARM3="
			+ parm3+"&amp;isAdd="
			+ getIsAdd()
			+ "' border='0' alt='' width='0' height='0'>");
	}
	isSended = '1';
}

// 判断是否加投
function getIsAdd(){
	if (getCounterCookie('tmg_counter') == 'tmg20100223' || document.referrer.indexOf('base_new') != -1) {
		return 1;
	}
	else if (getCounterCookie('tmg_counter') == 'tmg20100226') {
			return 2;
	}else {
			return -1;
	}
}

/**
 * 得到唯一标识符 UserID
 */
function getUID(){
	var chars = Math.random().toString().split('.')[1].split('');
	var randomId = '';
	while(chars.length != 19){
		if(chars.length < 19)
			chars[chars.length] = Math.round(Math.random());
		else if(chars.length > 19)
			chars[chars.length] = null;
	}
	for(var i = 0 ; i < 19 ; i++){
		randomId += chars[i];
	}
	var dateTemp = (new Date()).valueOf(); // 获得 13 位时间戳
	return dateTemp + randomId;
}

/**
 * 添加、修改 Cookie 
 * @param {Object} key
 * @param {Object} value
 * @param {Object} time	 分钟
 */
function addCookie(key,value,time){
    var exp = new Date();    // new Date("December 31, 9998");
    exp.setTime(exp.getTime() + time*60*1000);   // exp.getTime() 得到的是时间戳
	// expires 指定有效时间：格林威治标准时间 (GMT)。document.cookie = 'key=xx'：没有添加，如没有找到该 key 直接就修改其值
    document.cookie = key + "="+ escape (value) + ";expires=" + exp.toUTCString(); // "05 Jan 1996 00:00:00 GMT"。
}

/**
 * 删除 Cookie
 * @param {Object} key
 */
function deleteCookie(key){
	var exp  = new Date();
    exp.setTime(exp.getTime());
    document.cookie = key + "="+''+ ";expires=" + exp.toUTCString();
}

/**
 * 得到 Cookie
 * @param {Object} key
 */
function getCounterCookie(key){
	var arr = document.cookie.match(new RegExp("(^| )" + key + "=([^;]*)(;|$)")); // 直接得到 Cookie
	if(arr != null) 
		return unescape(arr[2]); 
	return null;
}

/**
 * 设置 Cookie 有效时间
 * @param {Object} time 分钟
 */
function setCookieTime(key,time){
	var exp  = new Date();
    exp.setTime(exp.getTime() + time*60*1000);
    document.cookie = key + "="+ escape (getCounterCookie(key)) + ";expires=" + exp.toUTCString();
}


/**
 * 获取当前的浏览器的名称
 */
function getBrowser(){
  	var sAgent = navigator.userAgent.toLowerCase();
	var browserName = '';
	if(sAgent.indexOf("msie")!=-1){
		browserName = 'IE';
		try{
			var sAgent = navigator.userAgent.toLowerCase();
			var a = sAgent.indexOf('msie');
			for(var i = 4 ; i < 8 ; i++){
				browserName += sAgent.charAt(a+i);
			}
		}catch(e){
			browserName = 'IE';
		}
	}else if(sAgent.indexOf("firefox")!=-1){
		browserName = 'firefox';
	}else if(sAgent.indexOf("chrome")!=-1){
		browserName = 'chrome';
	}else if(sAgent.indexOf("safari")!=-1){
		browserName = 'safari';
	}else if(sAgent.indexOf("opera")!=-1){
		browserName = 'opera';
	}else if(sAgent.indexOf("netscape")!=-1){
		browserName = 'netscape';
	}else if(sAgent.indexOf("maxthon")!=-1){
		browserName = 'maxthon';
	}else{
		browserName = 'other';
	}
	return browserName;
}

/**
 * 得到系统默认语言
 */
function getSystemLanguage(){
	var language = '';
	if(getBrowser().indexOf('IE') != '-1'){
		language = navigator.browserLanguage;
	}else if(getBrowser() == "firefox" || getBrowser() == "chrome"){
		var language = '';
		var sAgent = navigator.userAgent.toLowerCase();
		var sign = '';
		if(sAgent.indexOf('rv') != -1)
		sign = '; r';
		else 
		sign = ')';
		var a = sAgent.indexOf('nt');
		var b = sAgent.indexOf(sign);
		var sum = '';
		
		for(var i = 0;i < b - a + 2 && b - a > 0; i++){
			sum += sAgent.charAt(a + i);
		}
		var a_ = sum.indexOf('; ') + 2;
		var b_ = sum.length - 2;
		for(var n = 0;n < b_ - a_ && b_ - a_ > 0; n++){
			language += sum.charAt(a_ + n);
		}
	}else{
		language = "undefind```111";
	}
	return language;
}

/**
 * 获取 flash 版本
 */
function getFlashVesion() { 
	var f = "-", n = navigator; 
	if (n.plugins && n.plugins.length) {
	    for (var ii = 0; ii < n.plugins.length; ii++) {
	          if (n.plugins[ii].name.indexOf('Shockwave Flash') != -1) { 
	              f = n.plugins[ii].description.split('Shockwave Flash ')[1]; 
	              break; 
	         } 
	    } 
	} else if (window.ActiveXObject) { 
	     for (var ii = 10; ii >= 2; ii--) { 
	        try { 
	           var fl = eval("new ActiveXObject('ShockwaveFlash.ShockwaveFlash." + ii + "');"); 
	           if (fl) { 
	               f = ii + '.0'; 
	              break; 
	           }
	        } catch (e) { 
	       } 
	   } 
	}
	return f; 
}

/**
 * 获取屏幕分辨率
 */
function getResolution(){
	return (screen.width + '*' + screen.height);
}

/**
 * 获取颜色位数
 */
function getColorDepth(){
	return screen.colorDepth;
}

/**
 * 获取操作系统
 */
function getSystem(){
	var sys	= '';
	try{
		var sAgent = navigator.userAgent.toLowerCase();
		var a = sAgent.indexOf('windows');
		for(var i = 0 ; i < 14 ; i++){
			sys += sAgent.charAt(a+i);
		}
	}catch(e){
		sys = navigator.platform;
	}
	return sys;
}
