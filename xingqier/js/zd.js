function $(element){
return element = document.getElementById(element);
}
function $D(canshu){
var d=$(canshu);
var h=d.offsetHeight;
var maxh=300;
function dmove(){
h+=200; //层展开速度
if(h>=maxh){
d.style.height='auto';
clearInterval(iIntervalId);
}else{
d.style.display='block';
d.style.height=h+'px';
}
}
iIntervalId=setInterval(dmove,2);
}
function $D2(canshu){
var d=$(canshu);
var h=d.offsetHeight;
var maxh=300;
function dmove(){
h-=200;//层收缩速度
if(h<=0){
d.style.display='none';
clearInterval(iIntervalId);
}else{
d.style.height=h+'px';
}
}
iIntervalId=setInterval(dmove,2);
}
function $use(test,canshu,font){
var d=$(canshu);
var sb=$(test);
var ab=$(font);

if(d.style.display=='block'){
	$D2(canshu);
	sb.innerHTML='<img src="../images/right.png"/> <style>#'+test+'{color:#ffb71c;  border-bottom:1px #ffb71c solid;}#'+font+'{color:#ffb71c;}</style>'; 
}else{
	
	$D(canshu);
sb.innerHTML='<img src="../images/down.png"/>  <style>#'+test+'{color:#ffffff; background-color:#ffb71c}#'+font+'{color:#ffffff;}</style>';


}
}// JavaScript Document
