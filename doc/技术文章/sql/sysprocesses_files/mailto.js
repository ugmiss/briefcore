//Final version 1.000.

function EMailStream(obj)
{
var stream;
if(document.body.innerHTML.search(/fbRating/) != -1)  {fbReload();} 

// *****Start: Localization Section************************  

var fbTitle_Text = '�ĵ�����';
var fbParagraph_Text = 'SQL Server �ĵ��༭���޷��ش��йؼ���֧�ַ�������⣬�����ǻ�ӭ�����ĵ���ʾ�����������顣����ֱ��ʹ��������ı��������ٷ��͵����ʼ��������ṩ�������ʱ����ʹ��Ӣ�ġ����輼��֧�֣���μ�<A HREF="MS-ITS:hlpview.chm::/addresources_62lv.htm">SQL Server ������Դ</A>��';
	//---Note to localization: Do not change <A> and </A> tags.---
var fbRateThisTopic_Text = '��Ը������������� (1-5)��';
var fbPoor_Text = "�ܲ�";
var fbExcellent_Text = "�ܺ�";
var fbEnterFeedbackHere_Text = '���ڴ��������ķ��������';
var fbCancel_Text = 'ȡ��';

// ******End: Localization Section************************
// *****(There is another localization section below.)*****

stream = '<DIV ID="feedbackarea">'
	+ '<br>'
	+ '<hr COLOR="#99CCFF">'
	+ '<H6 STYLE="margin-top:0.5em;">' + fbTitle_Text + '</H6>'
	+ '<P>' + fbParagraph_Text + '</P>'
	+ '<FORM METHOD="post" ENCTYPE="text/plain" NAME="formRating">'
	+ '<P>' + fbRateThisTopic_Text + '&nbsp;&nbsp;&nbsp;&nbsp;'
	+ fbPoor_Text + '&nbsp;<INPUT TYPE="radio" value="1" NAME="fbRating">'
	+ '<INPUT TYPE="radio" value="2" NAME="fbRating">'
	+ '<INPUT TYPE="radio" value="3" NAME="fbRating">'
	+ '<INPUT TYPE="radio" value="4" NAME="fbRating">'
	+ '<INPUT TYPE="radio" value="5" NAME="fbRating">&nbsp;' + fbExcellent_Text + '</P>'
	+ '</FORM>'
	+ '<P>' + fbEnterFeedbackHere_Text + '&nbsp;&nbsp;&nbsp;&nbsp;'
	+ '<SPAN ONCLICK="feedbackarea.style.display=\'none\';' + obj.id + '.innerHTML=\'\'">'+ submitFeedback() + '</SPAN></P>'
	+ '<P STYLE="width:100%;position:relative;float:left;clear:left;margin-bottom:-0.7em;margin-top:0em;" align=right><A HREF="#Feedback" ONCLICK=EMailStream(' + obj.id + ')>' + fbCancel_Text
	+ '</A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</P>'
	+ '<hr COLOR="#99CCFF">'
	+ '</div>';

obj.innerHTML = stream;
}

function submitFeedback()
{

// *****Start: Localization Section**********************

var fbTypeHere_Text = '���ڴ��������ķ������������ı������С�'
var fbSubmit_Text = '�ύ����';

// ******End: Localization Section***********************


  var sRecipient = "mailto:sqldocfb@microsoft.com";
  var sTitle = ParseTitle(document.title);
  var sCHM = ParseFileName(window.location.href,1);
  var sHTM = ParseFileName(window.location.href,2);
  var sLang = navigator.systemLanguage;
  

  var sSubject =  sTitle + ' (' + sCHM + '::/' + sHTM + '>>'  
	+ '\' + GetRating() + \':' + sLang + ')'; 

  var sEntireMailMessage = sRecipient + '?subject=' + sSubject 
	+ '&body=---' + fbTypeHere_Text + '---';

  var sHREF = '<A HREF=\"' + sRecipient + '" ONCLICK=\"this.href=\''
	+ sEntireMailMessage + '\';\">' + fbSubmit_Text + '</A>';

  return sHREF;
}

//---Parses document title.---
function ParseTitle(theTitle)
{
	theTitle = theTitle.replace(/\"/g,"--");
  	theTitle = theTitle.replace(/'/g,"-");
	if (theTitle == "") {theTitle = "Documentation Feedback";}
	if (theTitle.length > 60) {theTitle = theTitle.slice(0,57) + "...";}
	return theTitle;
}

//---Parses document filename.---
function ParseFileName(Filename, theNum)
{
  	var intPos = Filename.lastIndexOf("\\");
  	var intLen = Filename.length;
  	var newFileName = Filename.substr(intPos + 1, intLen  - intPos)
  	newFileName = newFileName.replace(/#Feedback/g,"");
  	var x = newFileName.lastIndexOf("/");

	if (theNum == 1) {newFileName = newFileName.substr(0, (x-2));}
	if (theNum == 2) {newFileName = newFileName.substr(x + 1);}

  	return(newFileName);
}

function GetRating()
{
	sRating = "0";
	for(var x = 0;x < 5;x++)
  	{
      		if(document.formRating.fbRating(x).checked) {sRating = x + 1;}
  	}
	return sRating;
}

//---Reloads window.---
function fbReload()
{
	window.location.reload(true);

}

<br> This file is decompiled by an unregistered version of ChmDecompiler. <br>
 Regsitered version does not show this message. <br>You can download ChmDecompiler at :
    <a href="http://www.zipghost.com/" target=_blank> http://www.zipghost.com/ </a>
    <br><br>
