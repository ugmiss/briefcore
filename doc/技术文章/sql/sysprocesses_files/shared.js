// Final version 1.000: 07/07/00 (BF)

// ***************************
// SECTION BEGIN: LOCALIZATION
// Localization must make changes to the following section.
// ***************************

// TEXT
// ---ALT text---
var L_ExpandCollapseAlt_ToolTip = '展开或折叠文本';	// expand
var L_GlossaryAlt_ToolTip = '查看定义';			// glossary
var L_NoteImgAlt_AlternativeText = '';				// note
var L_ImportantImgAlt_AlternativeText = '';			// important
var L_CautionImgAlt_AlternativeText = '';			// caution
var L_WarningImgAlt_AlternativeText = '';			// warning
var L_RelTopAlt_AlternativeText = '请参见';			// related topics
var L_KeyboardAlt_AlternativeText = '键盘快捷键';	// keyboard shortcuts
var L_FeedbackAlt_AlternativeText = '请您将有关本页的反馈意见发送给我们'; 	// feedback

// ---Glossary-related text---
var L_NoDefAlertTitle_Text = '定义不存在';
var L_NoDefAlert_Text = '该词汇和定义目前尚不存在于词汇表文件中。';
var L_BrowserAlert_Message = '您必须使用 IE4 或更高版本，才能查看弹出式词汇定义。';  		// downlevel browsers message
var L_See_Text = '<B>请参见：&nbsp;</B>';
var L_SeeAlso_Text = '<B>请参见：&nbsp;</B>';

//---Boilerplate text---
var L_CopyrightPrelim_Text = '本文档信息仅为初稿，因此内容可能不够完善，尚需进一步修订。';
var L_CopyrightFinal_Text = '&copy;1988-2000 Microsoft Corporation。保留所有权利。';

// ---Other text---
var L_RelTopHeader_Text = '<P><B>请参见</B></P>';	// related topics header text
var L_GraphicClickText_Text = '放大关系图';		// hot text under thumbnail graphics
var L_Error_Text = '加载 HTML 文件时发生错误。'; // linked-file failure message

// SECTION END: LOCALIZATION

// *********************
// SECTION BEGIN: STYLES
// *********************

// ---Notes---
var L_NotesAlign_Style = 'STYLE="MARGIN-LEFT:0EM;"';			// IMG element alignment
var L_NotesLTR_Style = 'STYLE="margin-left :1.5em; margin-top:-1.1em;"';	// P element style
var L_NotesRTL_Style = 'STYLE="margin-right:1.5em; margin-top:-1.1em;"';	// P element style

// ---Related topics popup and icon alignment and font size---
var L_RelPopAlign1_Style    = 'STYLE="margin-top:-1.4em; margin-right:1.6em;"';
var L_RelPopAlign1RTL_Style = 'STYLE="margin-top:-1.4em; margin-left:1.6em;"';
var L_RelPopAlign1A_Style   = 'STYLE="margin-top:-1.4em;"';
var L_RelPopFontSize_Style = '100%';
var L_RelPopPClassRight_Style = 'STYLE="text-align:right;"';
var L_RelPopPClassLeft_Style  = 'STYLE="text-align:left;"';

// ---FAQ Expand alignment---
var L_ExCoImgAlign_Style	= 'margin-left:-10px';
var L_ExCoImgAlignRTL_Style	= 'margin-right:-10px;';

// ---Popup box style---
var L_PopUpBoxStyle_Style = 'visibility:hidden; position:absolute; top:0px; left:0px; width:200px; z-index:2; padding:10px; background-color:#FFFFCC; border:solid 1 #333333;';

// SECTION END: STYLES

// **********************************
// SECTION BEGIN: FUNCTIONAL SETTINGS
// **********************************

var InlineImgOn	= true;	// if true, inline gifs appear left of glossary and expand/collapse links
var xmlGloss = true;		// selects XML/TXT glossary
var InsertBoilerplateText = true;	// if true, script inserts boilerplate

// SECTION END: FUNCTIONAL SETTINGS

// *****************************
// SECTION BEGIN: FILE LOCATIONS
// *****************************

//#####These locations vary between projects.#####

var moniker = 'MS-ITS:';		
var NameOfCHM = 'ExchSvrs.chm';
var GlossaryCHM = 'MS-ITS:glossary.chm::/';
var xmlFile = GlossaryCHM + 'Basics/Gloss.xml';
bpKeyboardShortcutLink = 'MS-ITS:uiref.chm::/uiref_4xpu.htm';
bpCopyrightLink = 'MS-ITS:hlpview.chm::/gscopy_0z3m.htm';
bpPreliminary_Switch = 0;	// 1 = preliminary copyright text, 0 = no such text
bpKeyboard_Switch = 1;		// 1 = keyboard shortcuts icon, 0 = no icon
bpFeedback_Switch = 1;		// 1 = feedback icon, 0 = no icon

//################################################

// ---Basic locations.---

var BeforeReference = "/";
var CHMRef = '';					// thumbnail windows base .chm 
var GifLocation = '';

// ---.gifs for the possible expand states---
var closed    = BeforeReference + 'Basics/coe.gif';
var expand    = BeforeReference + 'Basics/coc.gif';
var closedHot = BeforeReference + 'Basics/coeb.gif';
var expandHot = BeforeReference + 'Basics/cocb.gif';

// ---.gifs for the possible related topics button states---
var InnerNavCold  = BeforeReference + 'Basics/relglyph.gif';		// onmouseout
var InnerNavHot   = BeforeReference + 'Basics/relglyph_.gif';		// onmouseover
var InnerNavClick = BeforeReference + 'Basics/relglyph_c.gif';		// onclick

//---.gifs for the possible feedback and keyboard icon states---
var IconKBCold  = BeforeReference + 'Basics/keybrd.gif';	
var IconKBHot  = BeforeReference + 'Basics/keybrd_.gif';	
var IconKBClick  = BeforeReference + 'Basics/keybrd_c.gif';	
var IconFBCold  = BeforeReference + 'Basics/mailto.gif';	
var IconFBHot  = BeforeReference + 'Basics/mailto_.gif';	
var IconFBClick  = BeforeReference + 'Basics/mailto_c.gif';	

// ---CSS styles with file names (inserted later)---
var CSSInsertion =
	'<STYLE><!--\r\n'
	+((InlineImgOn)?'':'DIV.EXPAND,DIV.EXPAND2,DIV.EXPAND3 {MARGIN-LEFT:1EM; MARGIN-RIGHT:1EM;}')	
	+'\r\n// --></STYLE>';

	// Insert these CSS styles.
	document.write(CSSInsertion);


// SECTION END: FILE LOCATIONS

// ***********************************
// SECTION BEGIN: VARIABLE DEFINITIONS
// ***********************************

// ---"GLOSSARY POPUP BROWSER CHECK VARIABLE"---
var browser = navigator.appName + ' ' + parseInt(navigator.appVersion);

// ---Globals---
var	theTimeOut,
	ReltopicS,
	TermDef,
	tiC,
	WorkText,
	AnotherTagIndex,
	noPopups,
	ieX,
	ieY,
	popOpen,
	theImg,
	theDiv,
	PopUp_InAPopUp,
	e,
	AnotherCheck,
	WindowOne,
	WindowTwo,
	theTagIndex,
	theTagIndex2,
	sParamFILE,
	lastClickedPopUp,
	IE4,
	IE5,
	checkIt2,
	checkIt,
	LiNker,
	HoldBookTitLe,
	HoldBookTitLe2,
	WindowIsLoading,
	linkedFileID,
	LinkedFileNum,
	intervalID;

// ---HTML Help OCX declaration for graphic and procedure windows---
var ActX = 'TYPE="application/x-oleobject" CLASSID="clsid:adb880a6-d8ff-11cf-9377-00aa003b7a11"';

// ---Browser check for popup support---
if (
		(browser == 'Microsoft Internet Explorer 4')
		|| (browser == 'Microsoft Internet Explorer 5')	)
	IE4 = true;
else
	noPopups = true;
if (
		(navigator.appVersion.indexOf('MSIE 5') > 0)
		|| 
		(
			(navigator.appVersion.indexOf('MSIE') > 0
			&& parseInt(navigator.appVersion) >= 5)
		)
	)
	IE5 = true;

// ---Linked files---
var inCr;
function public_get_innerHTML(){return document.body.innerHTML;}	// used when accessing linked files
var tiC = 0;														// stores initial timeout for linked file

var defContent = '';	// "picks up and holds glossary definition from txt file"

// ---XML popup support---
var xmlTermRoot;      	  // XML term root variable (move into function later)
var xmlDOM; 			  // The XMLDOM ActiveX object model
var xmlDOMLoaded = false; // Not loaded til 1st glossary popup. Persists for see alsos

// SECTION END: VARIABLE DEFINITIONS

// ************************************
// SECTION BEGIN: HTML TEXT DEFINITIONS
// ************************************

// ---Other definitions---
var SpacerGiffy = BeforeReference + 'Basics/spacer.gif';	// universal spacer image (1x1 square)
var LtrOrRtlFloat = 'float:none;'	// thumbnail style used on onload()

var _Cold  = '#0033BB';			// mouseout text color
var _Hot   = '#FF6600';			// mouseover text color
var _Click = '#FF6600';			// glossary click text color

// ---Inline images' html (set .gif height and width)---
if (InlineImgOn)	
{
// ---Expand IMG----
	var initialClosed =									
		'<IMG CLASS="ExPand" '
		+((document.dir == 'rtl')?'STYLE="FILTER: flipH;" ':'')		//
		+'SRC="'
		+BeforeReference
		+'Basics/coe.gif" HEIGHT="9" WIDTH="12" ALT="'
		+L_ExpandCollapseAlt_ToolTip
		+'" BORDER="0">';
	var initialClosed_B =
		'<IMG CLASS="ExPand" STYLE="'
		+((document.dir == 'rtl')?L_ExCoImgAlignRTL_Style+' FILTER: flipH;':L_ExCoImgAlign_Style)	//
		+'" SRC="'
		+BeforeReference
		+'Basics/coe.gif" HEIGHT="9" WIDTH="12" ALT="'
		+L_ExpandCollapseAlt_ToolTip
		+'" BORDER="0">';
// ---Glossary IMG---
	var glossInitial = '';					
}

// ---Popups---
var popupDIV =						// glossary popup inserted onload()
	'<DIV ID="popUpWindow" STYLE="'
	+L_PopUpBoxStyle_Style
	+'"></DIV>';
var RelPopupDiV =					// related topics popup inserted onload()
	'<DIV ID="RelpopUpWindow" STYLE="'
	+L_PopUpBoxStyle_Style
	+'"></DIV>';

// ---Related topics---
var InnerNavInitial =				// related topics IMG for pages with related topics
	'<IMG CLASS="HIDEREL"'
	+((document.dir == 'rtl')?' STYLE="FILTER: flipH;"':'')	// identical per original code: correct???
	+' SRC="'
	+BeforeReference
	+'Basics/relglyph.gif" HEIGHT="18" WIDTH="28" ALT="'
	+L_RelTopAlt_AlternativeText
	+'" BORDER="0">';


var RelPopSpacerGif =				// placeholder IMG for pages without related topics
	'<IMG CLASS="HIDEREL" SRC="'
	+SpacerGiffy
	+'" HEIGHT="18" WIDTH="28" ALT="" BORDER="0">';
var RelTOpSpacerGif =				// spacer used in laying out related topics at bottom of the page
	'<IMG STYLE="DISPLAY:NONE;" SRC="'
	+SpacerGiffy
	+'" HEIGHT="18" WIDTH="28" ALT="" BORDER="0">';

// ---Notes, etc.---
var noteImg =					// note IMG
	'<IMG '
	+L_NotesAlign_Style
	+' SRC="'
	+BeforeReference
	+'Basics/note.gif" HEIGHT="11" WIDTH="12" ALT="'
	+L_NoteImgAlt_AlternativeText
	+'" BORDER="0">';
var importantImg =				// important IMG
	'<IMG '
	+L_NotesAlign_Style
	+' SRC="'
	+BeforeReference
	+'Basics/important.gif" HEIGHT="11" WIDTH="12" ALT="'
	+L_ImportantImgAlt_AlternativeText
	+'" BORDER="0">';
var cautionImg =				// caution IMG
	'<IMG '
	+L_NotesAlign_Style
	+' SRC="'
	+BeforeReference
	+'Basics/caution.gif" HEIGHT="11" WIDTH="12" ALT="'
	+L_CautionImgAlt_AlternativeText
	+'" BORDER="0">';
var warningImg =				// warning IMG
	'<IMG '
	+L_NotesAlign_Style
	+' SRC="'
	+BeforeReference
	+'Basics/warning.gif" HEIGHT="11" WIDTH="12" ALT="'
	+L_WarningImgAlt_AlternativeText
	+'" BORDER="0">';

//---Boilerplate text---

var bpIconRelTopics = 
	'<P ID="TiTLE"></P>';


var bpIconKeyb =
	'<A HREF="'
	+ bpKeyboardShortcutLink
	+ '" ID="IconKB">'
	+ '<IMG name="keysho" style="CURSOR:hand;margin-top:1px;margin-right:2px;'
	+ 'margin-left:2px;left:0;"'
	+ ' alt="' + L_KeyboardAlt_AlternativeText + '"'
	//+ ' src="MS-ITS:' + NameOfCHM + '::/Basics/keybrd.gif"></A>';
	+ ' src="Basics/keybrd.gif"></A>';

var bpIconFeedb =
	'<A HREF="#Feedback" ID="IconFB"><IMG name="feedb" onclick=EMailStream(fb)'
	+' style="CURSOR:hand;margin-top:1px;margin-left:0px;"'
	+' alt="' + L_FeedbackAlt_AlternativeText + '"'
	//+' src="MS-ITS:' + NameOfCHM + '::/Basics/mailto.gif"></A>';
	+' src="Basics/mailto.gif"></A>';


if ((bpKeyboard_Switch == 1) && (bpFeedback_Switch == 1))
{
	bpIcons = bpIconFeedb + bpIconKeyb + bpIconRelTopics;
	bpRelTopicsPopupX = 62;
}
if ((bpKeyboard_Switch == 0) && (bpFeedback_Switch == 1))
{
	bpIcons = bpIconFeedb + bpIconRelTopics;
	bpRelTopicsPopupX = 30;
}
if ((bpKeyboard_Switch == 1) && (bpFeedback_Switch == 0))
{
	bpIcons = bpIconKeyb + bpIconRelTopics;
	bpRelTopicsPopupX = 34;
}
if ((bpKeyboard_Switch == 0) && (bpFeedback_Switch == 0))
{
	bpIcons = bpIconRelTopics;
	bpRelTopicsPopupX = 2;
}

bpKeyboard_Switch = 0;		// 1 = keyboard shortcuts icon, 0 = no icon
bpFeedback_Switch = 0;	

var bpTopOfFile1 = 
	'<div id="nsbanner" class="nsbanner"><div id="bannerrow2" class="bannerrow2">'
	+ '<TABLE CLASS="buttonbartable" CELLSPACING=0><TR ID="hdr" NOWRAP>'
	+ '<TD width=95 NOWRAP>'
	+ bpIcons
	+ '</TD><TD NOWRAP>';

var bpTopOfFile2 = 
	'</TD></TR></TABLE></div></div>';

var bpCopyrightPrelim = 
	'\r<BR><CENTER><P STYLE="width:100%;position:relative;float:left;clear:left;"><b>' + L_CopyrightPrelim_Text + '</b></P></CENTER>';

var bpCopyrightFinal = 
	'\r<CENTER><P STYLE="width:100%;position:relative;float:left;clear:left;"><A HREF="'
	+ bpCopyrightLink
	+'">'
	+ L_CopyrightFinal_Text
	+ '</A></P></CENTER>'
	+ '<H4><A NAME="feedback"></A></H4><SPAN id="fb"></SPAN>';


// END SECTION: HTML DEFINITIONS

// ***********************************
// BEGIN SECTION: FUNCTION DEFINITIONS
// ***********************************

//---Add linked file content.---


function displayLinkedDocument2(){
	var theLinkHREF;
	//--Get content of all linked files.--
	LinkedFileNum = 1;
	theLinkHREF = document.all['linkedFile1'].all.tags('A')(0).href.toLowerCase();
	LinkedObjectReload(theLinkHREF);
	}

function LinkedObjectReload(theLinkHREF)
{
	if (document.all['Scriptlet'] != null) 
	{
		document.all['Scriptlet'].outerHTML = '';
	}
	// ---Build and insert linked file object.---
	var sObjectText = '<OBJECT ID="Scriptlet'
		+'" STYLE="display:none;" TYPE="text/x-scriptlet" DATA="'
		+theLinkHREF
		+'"></OBJECT>';
	document.body.insertAdjacentHTML('beforeEnd', sObjectText);
	intervalID = window.setInterval(checkReadyState,10);
}

function checkReadyState() 
{
	if (document.all['Scriptlet'].object.readyState == 4) 
	{
		window.clearInterval(intervalID);
		Scriptlet_onreadystatechange();
	}
}

function Scriptlet_onreadystatechange()
{
	//---Check to see if the document has been loaded. If not, exit.--
	if (document.all['Scriptlet'].readyState == 4)
	{
	//---Variables.---
	var aaa = "";
	var sss = "";
	b = LinkedFileNum;
	var ItsIt = 'linkedFile' + b;
	var theSpot = 'Scriptlet';
	var objnum = 'hhobj_' + b + '0';
	var seeme;
	href = document.all[ItsIt].all.tags('A')(0).href.toLowerCase();
	seeme = document.all[theSpot].object.innerHTML;
	
	if (seeme == null)
	{
	seeme = '';
	}
	
	//--Remove "ends" from linked file.---
	var bbb = seeme.indexOf('<A name');
	var ccc= (seeme.lastIndexOf('<!--END-->'));
	if ((bbb != -1) && (ccc != -1))
	{
		var aaa = seeme.substring((bbb-4), ccc);
	}

	//Fix any ALinks present in the linked file.
	//---Get the ALink information.---
	var ppp = seeme.indexOf('hhobj_');
	if (ppp != -1) 
	{
		var qqq = seeme.lastIndexOf('<DIV',ppp);
		var rrr = seeme.indexOf('</DIV>',qqq);
		var sss = seeme.substring(qqq,(rrr+6));

		//--Get AName of current document, and deactivate AName links.--
		var temp1 = document.body.innerHTML;
		var temp2 = temp1.toLowerCase().indexOf('<a name=') + 8;
		var temp3 = temp1.toLowerCase().indexOf('</a>',temp2) - 1;
		var theAName = temp1.substring(temp2,temp3);
		var ttt = sss.indexOf(theAName);
		if (ttt != -1)
		{
			var uuu = sss.lastIndexOf('hhobj_',ttt);
			var vvv = sss.substring(uuu,(uuu + 8));
			if (vvv.substring(7) == " ") 
			{
				vvv = vvv.substring(0,7);
			}
			var www = aaa.indexOf(vvv);
			var xxx = aaa.lastIndexOf('<A href',www);
			var yyy = aaa.indexOf('>',xxx);
			var zzz = aaa.indexOf('</A>',xxx);
			aaa = aaa.substring(0,xxx)
				+ aaa.substring((yyy + 1),(zzz + 2))
				+ aaa.substring((zzz + 6),aaa.length);
		}
	}
			
	//---Add .chm name to .htm HREF links.---
	var aaaa = aaa.indexOf(".htm");
	if (aaaa != -1)
	{
		theCHM = document.all[theSpot].getAttribute('DATA');
		theCHM = theCHM.substring(0,(theCHM.indexOf('/') + 1));
		aaa = aaa.replace(/"([^M].{5,25}\.htm")/gi,'"MS-ITS:' + theCHM + "$1");
		aaa = aaa.replace(/MS-ITS:MS-ITS:/gi,'MS-ITS:');
	}
	
	
	//---Insert HTML.---
	seeme = sss + aaa;
	seeme = seeme.replace(/hhobj_/gi,objnum);
	var theDocument = document.all[ItsIt];
	theDocument.innerHTML = seeme;
	LiNker = false;
	
	//---Call next linked file.---
	incrementLinkedFileNum();
	}
}

function incrementLinkedFileNum()
{
	LinkedFileNum++;
	if (LinkedFileNum >= linkedFileID) 
	{
		return;
	}
	else 
	{
		b = LinkedFileNum;
		var ItsIt = 'linkedFile' + b;
		href = document.all[ItsIt].all.tags('A')(0).href.toLowerCase();
		LinkedObjectReload(href);
	}
}

// =====window.onload=====
function window_onload()
{
	WindowIsLoading = 1;
	if (InsertBoilerplateText == true) {InsertBoilerplate();}
	ProcessSPANTags();	// linked file objects created here
	ProcessATags();
	ProcessDIVTags();
	ProcessPTags();
	ProcessPopups();
	if ((IE4) && (LiNker)) {displayLinkedDocument2();}
	WindowIsLoading = 0;
}

	function InsertBoilerplate()
	{
	if (document.all['StartOfFile'] != null)
		{
		theTitle = document.all['StartOfFile'].innerHTML;
		document.all['StartOfFile'].outerHTML = 
			bpTopOfFile1 + theTitle + bpTopOfFile2;
		}
	if (document.all['EndOfFile'] != null)
		{
		if (bpPreliminary_Switch == 1) 
			{bpCopyrightAll = bpCopyrightPrelim + bpCopyrightFinal;}
		else 
			{bpCopyrightAll = bpCopyrightFinal;}

		document.all['EndOfFile'].outerHTML = bpCopyrightAll;
		}
	}

	function ProcessSPANTags()
	{
		inCr = 0;
		var colln = document.all.tags('SPAN');	// SPAN tags
		var i,imax = colln.length;
		var theIdIs;				// 'ScriptletX'
		var href;				// target A tag
		for (var i=0; i<imax; i++)
		{
			if (colln[i].id.indexOf('linkedFile') < 0)
				continue;	// skip to next element
			
			inCr++;	
		
			// --Grab the proper href (xxx.htm).---
			href = colln[i].all.tags('A')(0).href.toLowerCase();

			colln[i].outerHTML =
				'<SPAN ID="linkedFile'
				+inCr
				+'" CLASS="linkedfile"></SPAN>';

			colln[i].innerHTML =
				'<A HREF="' + href
				+'" STYLE="display:none"></A>';

		}
		LiNker = (inCr > 0);
		linkedFileID = inCr + 1;
	}

	function ProcessATags()
	{
		var colln = document.all.tags('A');
		var tmp;
		for (var i=0; i<colln.length; i++)
		{
			tmp = colln[i].id;
			if      (tmp.indexOf('ThumbNail'		) > -1)	ProcessATags_Thumbnail		(colln[i]);
			else if (tmp.indexOf('PopUp'			) > -1)	ProcessATags_Glossary		(colln[i]);
			else if (tmp.indexOf('ExPand'			) > -1)	ProcessATags_Expand			(colln[i]);
		}
	}
		function ProcessATags_Thumbnail(e,floatdir)
		{
			var sThumbnailImg = e.href.toLowerCase();
			var sAltText = e.title;
			sThumbnailImg = get_TheUrL(sThumbnailImg);
			e.innerHTML =
				'<IMG CLASS="thumbnail" SRC="'
				+moniker
				+sThumbnailImg
				+'" VSPACE="4" ALT="'
				+sAltText
				+'"><BR>'
				+L_GraphicClickText_Text
				+'<BR>';
			e.outerHTML =
				'<DIV CLASS="thumbnail" STYLE="'
				+floatdir
				+'" >'
				+e.outerHTML
				+'</DIV>';
		}
		function ProcessATags_Glossary(e)
		{
			e.title = L_GlossaryAlt_ToolTip;
			if (InlineImgOn)
				e.innerHTML = glossInitial + e.innerHTML;	// write glossary .gifs
		}
		function ProcessATags_Expand(e)
		{
			e.title = L_ExpandCollapseAlt_ToolTip;
			if (InlineImgOn)
				e.innerHTML = initialClosed + e.innerHTML;	// write expand gifs
		}

	function ProcessDIVTags()
	{	
		var colln = document.all.tags('DIV');
		var tmp,cls,contents;
		for (var i=0; i<colln.length; i++)
		{
			tmp = colln[i].id;

				// Display any text automatically highlighted by HTML Help Search:
				cls = colln[i].className.toLowerCase();
				if	(  (cls == 'expand' )
					|| (cls == 'expand1')
					|| (cls == 'expand2')
					|| (cls == 'expand3') )
				{
					contents = colln[i].innerHTML;
					if (   (contents.indexOf('<FONT'            ) > -1)
						&& (contents.indexOf('BACKGROUND-COLOR:') > -1) )
						colln[i].style.display = 'block';
				}
					
		}
	}

	function ProcessPTags()
	{
		var bIsRTL = document.dir.toLowerCase() == 'rtl';
		var s1 = '<P '+( (bIsRTL)?L_NotesRTL_Style:L_NotesLTR_Style )+'>';
		var s2 = '</P>';

		var colln = document.all.tags('P');
		var e;
		for (var i=0; i<colln.length; i++)
		{
			e = colln[i];
			switch(e.id)
			{
				case 'Alert_Caution'	: e.outerHTML = cautionImg   + s1 + e.innerHTML + s2; break;
				case 'Alert_Important'	: e.outerHTML = importantImg + s1 + e.innerHTML + s2; break;
				case 'Alert_Note'		: e.outerHTML = noteImg      + s1 + e.innerHTML + s2; break;
				case 'Alert_Warning'	: e.outerHTML = warningImg   + s1 + e.innerHTML + s2; break;
			}
		}
	}

	function ProcessPopups()
	{
		var bIsPopup = ( (IE4) && (document.all.item('LinKs')	!= null) );
		var sA;

		if (bIsPopup)									
		{
			ReltopicS = LinKs.innerHTML;
			sA = '<A HREF="#" ID="InnerNav" TITLE="' + L_RelTopAlt_AlternativeText+'">' + InnerNavInitial + '</A>';
		}
		else										
		{
			sA = RelPopSpacerGif;
		}
		
		if (document.all['TiTLE'] != null)
		{
		TiTLE.outerHTML = sA +TiTLE.innerHTML;
		}
		
		if (IE4)			// Insert popup boxes into the document
		{
			document.body.insertAdjacentHTML('beforeEnd', popupDIV);
			document.body.insertAdjacentHTML('beforeEnd', RelPopupDiV);
		}
	}

// ----------------------------
// ---Start of dynamic code.---
// ----------------------------

//---window.onmouseover---
// ONMOUSE-OVER ////////////////////////////////////////////////////
function document_onmouseover()
{
	if (WindowIsLoading == 1) {return;}
	e = window.event.srcElement;
	// +++
	for (var a = 0; a < 5; a++)	// +++
	{
		if ((e.tagName != 'A') && (e.parentElement != null))
			e = e.parentElement;
		var eID = e.id;
		
		if (eID.indexOf('ExPand') != -1)							// Expand/Collapse
		{
			if (InlineImgOn)
			{
				e.style.color = _Hot;
				var theDiv = GrabtheExpandDiv(e);	// locate the div
				theImg = getImage(e);
				if (theImg != null){theImg.src = (theDiv.style.display == 'block')?expandHot:closedHot;}
			}
			else
			{
				e.style.color = _Hot;
				e.style.textDecoration = 'underline';
			}
			break;
		}
		
		else if ( (eID.indexOf('PopUp') != -1) && (theTagIndex != e.sourceIndex) )		// Glossary (no action if already clicked)
		{
			e.style.color = _Hot;
			e.style.textDecoration = 'underline';
			break;
		}
		
		else if ( (eID.indexOf('InnerNav') != -1) && (theTagIndex2 != e.sourceIndex) )	// RelTopic (no action if already clicked)
		{
			theImg = getImage(e);
			if (theImg != null){theImg.src = InnerNavHot;}
			break;
		}

		if (e.id.indexOf('IconFB') != -1)
		{
			document.all('feedb').src = IconFBHot;
		}

		if (e.id.indexOf('IconKB') != -1)
		{
			document.all('keysho').src = IconKBHot;
		}	
	}
}

// ONMOUSE-OUT /////////////////////////////////////////////////////
function document_onmouseout()
{
	if (WindowIsLoading == 1) {return;}
	e = window.event.srcElement;

	// +++
	
	for (var a = 0; a < 5; a++)		// +++
	{
		if ((e.tagName != 'A') && (e.parentElement != null))
			e = e.parentElement;
		var eID = e.id;

		
		// Expand/Collapse:
		if (eID.indexOf('ExPand') != -1)
		{
			if (InlineImgOn)
			{
				e.style.color = _Cold;
				var theDiv = GrabtheExpandDiv(e);	// locate the div
				theImg = getImage(e);
				if (theImg != null){theImg.src = (theDiv.style.display == 'block')?expand:closed;}
			}
			else
			{
				e.style.color = _Cold;
				e.style.textDecoration = 'none';
			}
			break;
		}
		
		// Glossary: ("doesn't send if glossary term clicked and rolled over")
		else if ( (eID.indexOf('PopUp')!= -1) && (theTagIndex != e.sourceIndex) )
		{
			e.style.color = _Cold;
			e.style.textDecoration = 'none';
			break;
		}
		
		// Related Topic Link: ("doesn't send if reltopic icon clicked and rolled over")
		else if ( (eID.indexOf('InnerNav') != -1) && (theTagIndex2 != e.sourceIndex) )
		{
			theImg = getImage(e);
			if (theImg != null){theImg.src = InnerNavCold;}
			break;
		}
		if (e.id.indexOf('IconFB') != -1)
		{
			document.all('feedb').src = IconFBCold;
		}

		if (e.id.indexOf('IconKB') != -1)
		{
		document.all('keysho').src = IconKBCold;
		}	
	}
}

// ONCLICK /////////////////////////////////////////////////////////
function document_onclick()
{
	if (WindowIsLoading == 1) {return;}
	e = window.event.srcElement;
	var relPopupVisible = 0;
	
	// Hide any open popups:
	if (document.all.RelpopUpWindow.style.visibility == 'visible')
	{
		relPopupVisible = 1;
	}
	document.all.RelpopUpWindow.style.visibility = 'hidden';
	document.all.popUpWindow.style.visibility = 'hidden';
	
	// Reset popup state variables (used in onresize)
	WindowOne = false;		// popup
	WindowTwo = false;		// innernav
	
	// Reset glossary popup state variables
	popOpen		= false;	// is a Glossary popup visible
	PopUp_InAPopUp	= false;	// user clicks a see-also term
	AnotherCheck	= false;	// inline gifs are off and HTMLHelp added html

	// +++
	for (var a = 0; a < 5; a++)		// +++
	{
		if ( (e.tagName != 'A') && (e.parentElement != null) )
			e = e.parentElement;
		eID = e.id;
		
		// ("XML code allows TDC to coexist because of this")
		xmlTermRoot = '';
		if (e.hash != null)
			xmlTermRoot = e.hash;
		
		// Variables to control click color of glossary link text:
		if (  checkIt2  &&  ( (eID.indexOf('PopUp') == -1) || (eID.indexOf('In_PopuP') == -1) )  )
		{
			var changeBack = document.all(theTagIndex);
			changeBack.style.color = _Cold;
			changeBack.style.textDecoration = 'none';
			theTagIndex = false;
			checkIt2 = false;
		}

		// Variables to control click color of related topics icon:
		if (  checkIt  &&  (theTagIndex2 != -1)  &&  (eID.indexOf('InnerNav') == -1)  )
		{
			theImg = getImage2(theTagIndex2);
			if (theImg != null){theImg.src = InnerNavCold;}
			theTagIndex2 = false;
			checkIt = false;
		}

		if (e.id.indexOf('IconFB') != -1)
		{
			document.all('feedb').src = IconFBClick;
		}

		if (e.id.indexOf('IconKB') != -1)
		{
			document.all('keysho').src = IconKBClick;
		}

		if      (eID.indexOf('ThumbNail') != -1)	{	callThumbnail(e);			break;	}		// Thumbnail
		else if (eID.indexOf('ExPand') != -1)		{	callExpand(e,InlineImgOn);	break;	}		// Expand/Collapse

		if (eID.indexOf('InnerNav') != -1)					// reltopic
		{
			lastClickedPopUp = e;	// used by onresize()
			WindowTwo = true;
			checkIt = true;
			theTagIndex2 = e.sourceIndex;
			theImg = getImage2(theTagIndex2);
			if (theImg != null){theImg.src = InnerNavClick;}
		
			if (relPopupVisible == 1)
			{
				document.all.RelpopUpWindow.style.visibility = 'hidden';
				if (theImg != null){theImg.src = InnerNavCold;}
			}
			else
			{
				callRelatedTopicS(e);
			}
			break;
		}

		else if (eID.indexOf('PopUp') != -1)				// glossary
		{
			lastClickedPopUp = e;			// used by onresize()
			WindowOne = true;

			// Set variables to control click color of glossary link text:
			checkIt2 = true;
			theTagIndex = true;
			theTagIndex = e.sourceIndex;
			AnotherTagIndex = parseInt(theTagIndex);
			e.style.color = _Click;
			//e.style.textDecoration = 'underline';

			WorkText = e.innerHTML;			// Capture the html containing the term

			var bAddedHTML = WorkText.indexOf('BACKGROUND-COLOR:') != -1;	// has HTMLHelp added html
			if (InlineImgOn)	// if glossary image is on
			{
				if (bAddedHTML)
					GetTheRealTerm();
				else
					GetTheRealTerm2();
			}
			else				// if no image on
			{
				if (bAddedHTML)												// has HTMLHelp added html
				{
					AnotherCheck = true;
					GetTheRealTerm();
				}
				else
					TermDef = e.innerHTML;
			}

			// Now perform the term lookup:
			callGlossary(e, TermDef);
			break;
		}

		else if (eID.indexOf('In_PopuP') != -1)					// Glossary SEE ALSO
		{
			// Set variables to control click color of glossary link text:
			checkIt2 = true;
			theTagIndex = parseInt(AnotherTagIndex);
			var changeBack = document.all(theTagIndex);
			changeBack.style.color = _Click;
			//changeBack.style.textDecoration = 'underline';

			// Set a state variable and perform the term lookup:
			PopUp_InAPopUp = true;
			TermDef = e.innerHTML;
			callGlossary(e, TermDef);
			break;
		}
	}
}

// ONKEYPRESS //////////////////////////////////////////////////////
function document_onkeypress()
{
	if (WindowIsLoading == 1) {return;}
	if (window.event.keyCode == 27)
	{
		document.all.popUpWindow.style.visibility = 'hidden';
		popOpen = false;	// state variable
	}
	if (window.event.keyCode == 6)
	{
		window.focus();
		window.location = "#Feedback";
		EMailStream(fb);
	}	
	if (window.event.keyCode == 11)
	{
		window.focus();
		window.location = bpKeyboardShortcutLink;
	}
	if (window.event.keyCode == 19)
	{
		window.focus();
		if (document.all.RelpopUpWindow.style.visibility == 'visible')
		{
		document.all.RelpopUpWindow.style.visibility = 'hidden';
		document.all.tags('IMG')[2].src = InnerNavCold;
		}
		else
		{	
			document.all.tags('IMG')[2].click();
		}

	}
}

// ONRESIZE ////////////////////////////////////////////////////////
function window_onresize()
{
	if (WindowIsLoading == 1) {return;}
 	// Pre-shortcut code...
 	if(lastClickedPopUp)
 	{
 		var nClientWidth = document.body.clientWidth;
 		var nPopupWidth  = popUpWindow.style.pixelWidth;
 		var nLeft;		// popUpWindow.style.pixelLeft
 		var nTop;		// popUpWindow.style.pixelTop
		
		if (WindowOne)							// glossary popups
		{
			if (lastClickedPopUp.offsetParent.tagName.toLowerCase() == 'body')
			{
				if((nPopupWidth + lastClickedPopUp.offsetLeft) <= nClientWidth)
					nLeft = lastClickedPopUp.offsetLeft;
				else
					nLeft = ((nClientWidth - 10) > nPopupWidth)?(nClientWidth - nPopupWidth):10;
				nTop = lastClickedPopUp.offsetTop + lastClickedPopUp.offsetHeight + 1;
			}
			else if (lastClickedPopUp.offsetParent.offsetParent.tagName.toLowerCase() == 'body')
			{
				if((nPopupWidth + lastClickedPopUp.offsetLeft + lastClickedPopUp.offsetParent.offsetLeft) <= nClientWidth)
					nLeft = lastClickedPopUp.offsetLeft + lastClickedPopUp.offsetParent.offsetLeft;
				else
					nLeft = ((nClientWidth - 10) > nPopupWidth)?(nClientWidth - nPopupWidth):10;
				nTop = lastClickedPopUp.offsetHeight + lastClickedPopUp.offsetTop + 1 + lastClickedPopUp.offsetParent.offsetTop;
			}
			else if (lastClickedPopUp.offsetParent.offsetParent.offsetParent.tagName.toLowerCase() == 'body')
			{
				if((nPopupWidth + lastClickedPopUp.offsetLeft + lastClickedPopUp.offsetParent.offsetLeft + lastClickedPopUp.offsetParent.offsetParent.offsetLeft) <= nClientWidth)
					nLeft = lastClickedPopUp.offsetLeft + lastClickedPopUp.offsetParent.offsetLeft + lastClickedPopUp.offsetParent.offsetParent.offsetLeft;
				else
					nLeft = ((nClientWidth - 10) > nPopupWidth)?(nClientWidth - nPopupWidth):10;
				nTop = lastClickedPopUp.offsetHeight + lastClickedPopUp.offsetTop + 1 + lastClickedPopUp.offsetParent.offsetTop + lastClickedPopUp.offsetParent.offsetParent.offsetTop;
			}
			else
			{
				if ((popUpWindow.style.pixelLeft + nPopupWidth) > document.body.clientWidth)
					nLeft = document.body.clientWidth - nPopupWidth;
				if ((popUpWindow.style.pixelTop + popUpWindow.style.pixelHeight) > document.body.clientHeight)
					nTop = document.body.clientHeight - popUpWindow.style.pixelHeight;
			}
			if (nLeft != null)
				popUpWindow.style.pixelLeft = nLeft;		// popUpWindow.style.pixelLeft
			if (nTop != null)
				popUpWindow.style.pixelTop  = nTop;			// popUpWindow.style.pixelTop
		}
		
		// Related Topics popups
		else if (WindowTwo)
		{
			RelpopUpWindow.style.pixelLeft = (document.dir=='rtl')
												?10
												:nClientWidth - RelpopUpWindow.style.pixelWidth - 10;
			RelpopUpWindow.style.pixelTop = ((lastClickedPopUp.offsetTop) + (lastClickedPopUp.offsetHeight) + (1));
		}
	}
}

// parse the popup term (IF WorkText.indexOf('BACKGROUND-COLOR:') != -1)
function GetTheRealTerm()
{
	var c;
	var d = '</FONT>';
	var f = WorkText;
	if (!AnotherCheck)	// in this case we don't need to remove images (inline images are off)
	{					// AnotherCheck = false when inline gifs are off and HTMLHelp added html
		c = f.indexOf('width=12>');
		if (c == -1)
			return;
		f = f.substring((c+9),f.length);
	}
	f = f.replace(d,'');	// remove all but the term and the HTML from HTMLHelp
	
	// this removes HTML added by HTMLHelp, leaving the term
	var g = f.indexOf('<');
	var h = f.indexOf('>');
	if ( (g == -1) || (h == -1) )
		return;
	var i = f.substring(g,(h+1));
	f = f.replace(i,'');
	TermDef = f;
	return TermDef;
}

// parse the popup term when HTMLHelp has not added HTML (IF *NOT* WorkText.indexOf('BACKGROUND-COLOR:') != -1)
function GetTheRealTerm2()
{
	var c = WorkText.lastIndexOf('width=12>');
	if (c == -1)
		return;
	else
		TermDef = WorkText.substring((c+9),WorkText.length);
	return TermDef;
}

// FIND AREA TO EXPAND/COLLAPSE
function GrabtheExpandDiv(e)
{
	var theExpandDiv, sTagName, sClassName;
	for (var a = 0; a < 5; a++)	// +++
	{
		var theTag = e.sourceIndex + e.children.length + a;
		theExpandDiv = document.all(theTag);
		sTagName   = theExpandDiv.tagName;
		sClassName = theExpandDiv.className.toLowerCase();
		if 
		(
			( (
					   (sTagName == 'DIV')
					|| (sTagName == 'SPAN')
				) && (
					   (sClassName == 'expand')
					|| (sClassName == 'expand1')
					|| (sClassName == 'expand2')
					|| (sClassName == 'expand3')
			) ) || theTag == document.all.length
		)
			break;
	}
	return theExpandDiv;
}

// rollovers and Expand getImage
function getImage(e)
{
	var ee = e;
	for (var a = 0; a < 5; a++)		// +++
	{
		if ((ee.tagName != 'A') && (ee.parentElement != null))
			ee = ee.parentElement;
		if(ee.tagName == 'A')
			return ee.all.tags('IMG')(0);
	}
	return ee;		// +++
}

// handles gray state of Related Topic and Glossary popups
function getImage2()	// +++
{
	// +++
	var TheSpot;
	if ((checkIt) && (theTagIndex2 != -1))			// if Related Topic
		TheSpot = document.all(theTagIndex2);
	else if ((checkIt2) && (theTagIndex != -1))		// if Glossary
		TheSpot = document.all(theTagIndex);
	else
		return;
	
	for (var a = 0; a < 5; a++)		// +++
	{
		if ((TheSpot.tagName != 'A') && (TheSpot.parentElement != null))
			TheSpot = TheSpot.parentElement;
		if(TheSpot.tagName == 'A')
			return TheSpot.all.tags('IMG')(0);
	}
	return TheSpot;		// +++
}

// call the Thumbnail window
function callThumbnail()
{
	event.returnValue = false;						// kill event
	
	var eH = e.href.toLowerCase();
	sParamFILE = get_TheUrL(eH);
	sParamFILE = CHMRef + sParamFILE;
	
	if (document.hhThumbnail)						// if exists, delete
		document.hhThumbnail.outerHTML = '';
	
	var h =
		'<OBJECT ID="hhThumbnail" '
		+ActX
		+' STYLE="display:none"><PARAM NAME="Command" VALUE="Related Topics">'
		+'VALUE="<PARAM NAME="Item1" VALUE="$global_largeart;'
		+moniker
		+sParamFILE
		+'"></OBJECT>';
	
	document.body.insertAdjacentHTML('beforeEnd', h);	// create and activate
	document.hhThumbnail.hhclick();
}

// designed to get the URL out of 'funky'-style HREF fields
function get_TheUrL(sHREF)
{
	var spaces = /\s/g;
	var eH = unescape(sHREF);
	eH	= eH.replace(spaces,'');
	eH_	= eH.toLowerCase(); // added
	
	var sParamFILE = '';
	var sParamCHM  = '';
	
	var iFILE = eH_.lastIndexOf('file=');
	if (iFILE != -1)
		sParamFILE = eH.substring(iFILE+5, eH.length);
	
	var iCHM = eH_.lastIndexOf('chm=');
	if (iCHM != -1)
	{
		sParamCHM = eH.substring(iCHM+4, iFILE) + "::/";
		sParamFILE= sParamCHM + sParamFILE;
	}
	return sParamFILE;
}

// call Related Topics popup
function callRelatedTopicS()
{
	event.returnValue = false;									// kill event
	document.all.RelpopUpWindow.innerHTML      = L_RelTopHeader_Text + ReltopicS;
	document.all.RelpopUpWindow.style.fontSize = L_RelPopFontSize_Style;
	document.all.RelpopUpWindow.style.left = (document.dir == 'rtl')
												?10
												:(bpRelTopicsPopupX);
	document.all.RelpopUpWindow.style.top = document.all.InnerNav.offsetTop + document.all.InnerNav.offsetHeight + 4;
	document.all.RelpopUpWindow.style.visibility = 'visible';
//document.body.clientWidth - RelpopUpWindow.style.pixelWidth - 10

}

// call Glossary popup
function callGlossary()
{
	event.returnValue = false;							// kill event
	
	// set XY popup coordinates
	if (PopUp_InAPopUp)
	{
		ieX = document.all.popUpWindow.style.left;
		ieY = document.all.popUpWindow.style.top;
	}
	else
	{
		if (e.offsetParent.tagName.toLowerCase() == 'body')
		{
			ieX = e.offsetLeft;
			ieY = e.offsetTop + e.offsetHeight + 1;
		}
		else if (e.offsetParent.offsetParent.tagName.toLowerCase() == 'body')
		{
			ieX = e.offsetLeft + e.offsetParent.offsetLeft;
			ieY = e.offsetHeight + e.offsetTop + e.offsetParent.offsetTop + 1;
		}
		else if (e.offsetParent.offsetParent.offsetParent.tagName.toLowerCase() == 'body')
		{
			ieX = e.offsetLeft + e.offsetParent.offsetLeft + e.offsetParent.offsetParent.offsetLeft;
			ieY = e.offsetHeight + e.offsetTop + e.offsetParent.offsetTop + e.offsetParent.offsetParent.offsetTop + 1;
		}
		else
		{
			ieX = window.event.clientX;
			ieY = window.event.clientY + document.body.scrollTop;
		}
	}
	
	// if a popup window is open, pause then reissue function call
	if (popOpen)
		window.setTimeout('callGlossary()', 50);
	
	if (noPopups)	// if not IE4+
	{
		alert (L_BrowserAlert_Message);
		noPopups = false;
		return;
	}
	
	// if no popup open, proceed to display popup...
	if (IE4 && !popOpen)
		iePopup();
}

// build the popup window
function iePopup()
{
	// adjust if the popup will be offscreen
	var rightlimit = ieX + document.all.popUpWindow.offsetWidth;
	if (rightlimit >= document.body.clientWidth)
		ieX -= (rightlimit - document.body.clientWidth);
	
	// set and position popup
	document.all.popUpWindow.innerHTML    = '';
	document.all.popUpWindow.style.height = 0;
	
	if (xmlGloss)	// if glossary is in XML control
	{
		document.all.popUpWindow.innerHTML = getXMLPopupContent(xmlTermRoot); // load formatted popup content or not found
				
		// callback required to get popup height +++
		window.setTimeout ('iePopHeight()', 0);
		
		// set popup's XY coordinates
		document.all.popUpWindow.style.top  = ieY;
		document.all.popUpWindow.style.left = ieX;
		popOpen = true;		// state variable: popup is ready
		return (false);		// kill bubble
	}
	else			// else glossary is in TDC control
	{
		if (document.tdcGloss)						// if TDC exists, delete
			document.tdcGloss.outerHTML = '';
		
		var h =
			'<OBJECT ID="tdcGloss" CLASSID="clsid:333C7BC4-460F-11D0-BC04-0080C7055A83" VIEWASTEXT>'
			+'<PARAM NAME="DataURL" VALUE="'
			+BeforeReference
			+'Basics/gloss.txt"><PARAM NAME="UseHeader" VALUE="True">'
			+'<PARAM NAME="FieldDelim" VALUE=","><PARAM NAME="EscapeChar" VALUE="#"></OBJECT>';
		
		document.body.insertAdjacentHTML('beforeEnd', h);		// create TDC
		
		var RS = tdcGloss.recordset;
		RS.moveFirst();
		
		// default text when no term is found
		document.all.popUpWindow.innerHTML =
			'<H6 CLASS="GLOSSARY_ITEM">'
			+L_NoDefAlertTitle_Text
			+'</H6><P>'
			+L_NoDefAlert_Text
			+'</P>';
		
		while (!RS.EOF)
		{
			if (TermDef.toLowerCase() == RS.fields('Term').value.toLowerCase())		// if found, get def
			{
				defContent = RS.fields('Definition').value;
			
				if (RS.fields('SeeAlso').value)				// handle any See Also's
					if (RS.fields('SeeAlso').value.indexOf('~') != -1)
						ParseOtherDefs();
					else
						document.all.popUpWindow.innerHTML =
							'<H6 CLASS="GLOSSARY_ITEM">'
							+TermDef
							+'</H6><P>'
							+defContent
							+'</P><P ID="OtherDefs"><B>See Also:&nbsp;</B><A ID="In_PopuP" HREF="#">'
							+RS.fields('SeeAlso').value
							+'</A></P>';
				else
					document.all.popUpWindow.innerHTML =
						'<H6 CLASS="GLOSSARY_ITEM">'
						+TermDef
						+'</H6><P>'
						+defContent
						+'</P>';
				break;
			}
			RS.moveNext();
		}
		// callback required to get popup height +++
		window.setTimeout ('iePopHeight()', 0);

		// set popup's XY coordinates
		document.all.popUpWindow.style.top = ieY;
		document.all.popUpWindow.style.left = ieX;
		popOpen = true;		// state variable: popup is ready
		return false;		// kill bubble
	}
}

// handle case of multiple See Also's in a TDC file (not used by XML glossary)
function ParseOtherDefs()
{
	var FstStop		=  0;
	var FstWrd		= '';
	var AimPnt		= tdcGloss.recordset.fields('SeeAlso').value;
	var DefsString	= '<B>See Also:&nbsp;</B>';
	
	while ( (FstStop = AimPnt.indexOf('~')) > -1)		// loop through each See Also, separated by tildes ('~')
	{
		FstWrd		= AimPnt.substring(0, FstStop);
		DefsString	=
			DefsString
			+'<A ID="In_PopuP" HREF="gloss_all.htm#def_'
			+FstWrd
			+'">'
			+FstWrd
			+'</a>,&nbsp;';
		AimPnt = AimPnt.substring(FstStop + 1, AimPnt.length);
	}
	DefsString =
		DefsString
		+'<A ID="In_PopuP" HREF="gloss_all.htm#def_'
		+AimPnt
		+'">'
		+AimPnt
		+'</a>';
	document.all.popUpWindow.innerHTML =
		'<H6 CLASS="GLOSSARY_ITEM">'
		+TermDef
		+'</H6><P>'
		+defContent
		+'</P><P ID="OtherDefs">'
		+DefsString
		+'</P>';
}

// get popup height, nudge if necessary, and display popup
function iePopHeight()
{
	var pageBottom = document.body.scrollTop + document.body.clientHeight;
	var popHeight = document.all.popUpWindow.offsetHeight;
	document.all.popUpWindow.style.height = popHeight - 2 * (parseInt(document.all.popUpWindow.style.borderWidth));
	
	if (popHeight + ieY >= pageBottom)	// if popup longer than screen, move to top of screen
		document.all.popUpWindow.style.top = (popHeight <= pageBottom)?(pageBottom-popHeight):0;
	
	document.all.popUpWindow.style.visibility = 'visible';	// display popup
}

// show/hide Expand block
function callExpand(e,InlineImgOn)
{
	event.returnValue = false;			// kill bubble
	var theDiv = GrabtheExpandDiv(e);
	
	theDiv.style.display = (theDiv.style.display == 'block')?'none':'block';
	if (!InlineImgOn)
	{
		var theImg = getImage(e);
		if (theImg != null){theImg.src = (theDiv.style.display == 'block')?closed:expand;}
	}
	document.body.insertAdjacentHTML('beforeEnd','&nbsp;');
}

// XML POPUP CODE BEGINS
function getXMLPopupContent(theXMLTermRoot)
{
	var theXMLTerm, theXMLTermID;
	var theEntry;
	var theScopeDefs;
	var theScopes;
	var theDefinition;
	var theSeeAlsos, seeAlsoID, seeAlsoTerm;
	var theSeeEntry, seeID, seeTerm;
	var outText;
	var i, j, k, l, m;
	var scopeFound;
	var noDef =
		'<H6 CLASS="GLOSSARY_ITEM">'
		+L_NoDefAlertTitle_Text
		+'</H6><P>'
		+L_NoDefAlert_Text
		+'</P>';
	
	// CHECK THAT TERM ROOT EXISTS
	if (theXMLTermRoot.length > 1)
	{
		theXMLTermID = theXMLTermRoot.substring(1, theXMLTermRoot.length);
		i = theXMLTermID.indexOf(':');
		if (i > 0)
			theXMLTerm = theXMLTermID.substring(i+1, theXMLTermID.length);
		else
			return (noDef);
	}
	else
		return (noDef);
	
	if (!xmlDOMLoaded)	// load the XML object the first time through
	{
		xmlDOM = new ActiveXObject('Microsoft.XMLDOM');
		xmlDOM.async = false;
		xmlDOM.validateOnParse = false;
		xmlDOM.load(xmlFile);
		xmlDOMLoaded = true;		// state variable
	}

	outText = noDef;
	theEntry = xmlDOM.nodeFromID(theXMLTerm);	// look up term
	
	if (theEntry == null)
		outText = noDef;
	else							// term found
	{
		theScopeDefs = theEntry.selectNodes('scopeDef');
		scopeFound = false;
		
		for (i = 0; i < theScopeDefs.length && !scopeFound; i++)
		{
			theScopes = theScopeDefs(i).selectNodes('scope');
			for (j = 0; j < theScopes.length; j++)		// this loop could be replaced with a single XSL pattern
			{
				if (theScopes(j).attributes.getNamedItem('scopeTermID').text == theXMLTermID)	// checking for scopedef match
				{
					// FOUND SCOPE IN ENTERY - GET AND FORMAT THE TERM AND DEF DATA
					scopeFound = true;
					outText = formatXMLTerm(theEntry.selectSingleNode('term').text);
					if (theScopeDefs(i).selectSingleNode('def') != null)
					{
						theDefinition = formatXMLDef(theScopeDefs(i).selectSingleNode('def'));
						outText += theDefinition;				// get def
						
						theSeeAlsos = theScopeDefs(i).selectNodes('seeAlso');
						seeAlsoID = '';
						seeAlsoTerm = '';
						for (k = 0; k < theSeeAlsos.length; k++)	// search for See Alsos
						{
							seeAlsoID = theSeeAlsos(k).attributes.getNamedItem('seeAlsoID').text;
							l = seeAlsoID.indexOf(':');
							if (l > 0)
							{
								seeAlsoScope = seeAlsoID.substring(0, l+1);
								seeAlsoID    = seeAlsoID.substring(l+1, seeAlsoID.length);
							}
							else
								seeAlsoScope = '';
							seeAlsoTerm = xmlDOM.nodeFromID(seeAlsoID).selectSingleNode('term').text;
							
							// if k == 0, format first See Also
							outText += formatXMLSeeAlso(seeAlsoScope + seeAlsoID, seeAlsoTerm, (k == 0));
						}
						if (k > 0)
							outText = outText + '</P>';	// if See Alsos, add trailing paragraph mark
					}
					else
					{
						theSeeEntry = theScopeDefs(i).selectSingleNode('seeEntry');
						seeID = theSeeEntry.attributes.getNamedItem('seeID').text;
						k = seeID.indexOf(':');
						if (k > 0)
						{
							seeScope = seeID.substring(0, k+1);
							seeID    = seeID.substring(k+1, seeID.length);
						}
						else
							seeScope = '';
						seeTerm = xmlDOM.nodeFromID(seeID).selectSingleNode('term').text;
						outText += formatXMLSee(seeScope + seeID, seeTerm);
					}
				}//end if-scope-found block
			}
		}
	}
	xmlTermRoot = '';	//  clean up global
//  for debugging:  return ("XML -" + xmlTermRoot + "/" + theXMLTerm + "termText:" + termText + "defText:" + defText  + outText);
	return outText;
}

// helper functions to format the glossary text
function formatXMLTerm(theTerm)		// term
{
	return ('<H6 CLASS="GLOSSARY_ITEM">'+theTerm+'</H6>');
}
function formatXMLDef(theDef)		// def
{
	var theParas = theDef.selectNodes('para');
	var theDefOut = '';
	for (var i = 0; i < theParas.length; i++)
		theDefOut += '<P>'+theParas(i).text+'</P>';
	return theDefOut;
}
function formatXMLSee(theSeeTermID, theSeeTerm)	// see also
{
	var theSeeText;
	theSeeText = '<A ID="In_PopuP" HREF="#'+theSeeTermID+'">'+theSeeTerm+'</A>';
	return ('<P ID="SeeDef">'+L_See_Text+theSeeText);
}
function formatXMLSeeAlso(theSeeAlsoTermID, theSeeAlsoTerm, bFirstOne)	// see alsos
{
    var theSeeAlsoText =
		'<A ID="In_PopuP" HREF="#'
		+theSeeAlsoTermID
		+'">'
		+theSeeAlsoTerm
		+'</A>';
	return ( ((bFirstOne)?('<P ID="OtherDefs">'+L_SeeAlso_Text):(', '))  +  theSeeAlsoText );
}


// EVENT HANDLER HOOKS BEGIN

window.onload			= window_onload;
window.onresize			= window_onresize;
document.onkeypress		= document_onkeypress;
document.onclick		= document_onclick;
document.onmouseover		= document_onmouseover;
document.onmouseout		= document_onmouseout;

// legacy event handler names
function loadInitialThings()		{ window_onload(); }
function fixPopUps()			{ window_onresize(); }
function ieKey()			{ document_onkeypress(); }
function clickAndDo()			{ document_onclick(); }
function gettingHot()			{ document_onmouseover(); }
function gettingCold()			{ document_onmouseout(); }

// EVENT HANDLER HOOKS END
<br> This file is decompiled by an unregistered version of ChmDecompiler. <br>
 Regsitered version does not show this message. <br>You can download ChmDecompiler at :
    <a href="http://www.zipghost.com/" target=_blank> http://www.zipghost.com/ </a>
    <br><br>
