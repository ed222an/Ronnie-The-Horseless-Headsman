(function(m){var window=this;var bW=function(){this.k=(0,m.I)("watch-headline-title");this.R=(0,m.J)("watch-title",this.k);this.j=(0,m.I)("watch-headline-title-form");this.$=(0,m.J)("yt-alert-content",this.j);this.da=(0,m.I)("watch-headline-title-reset");this.A=(0,m.I)("action-panel-details");this.fa=(0,m.I)("watch-description");this.b=(0,m.I)("watch-video-info-form");this.ia=(0,m.J)("yt-alert-content",this.b);this.pa=(0,m.I)("watch-video-info-submit");this.oa=(0,m.I)("watch-video-info-reset");this.N=(0,m.I)("watch-privacy-icon");
this.V=(0,m.I)("watch-description-toggle");this.K=!1;this.D=(0,m.J)("eow-description-textarea",this.b);this.G=(0,m.J)("eow-privacy-select",this.b);this.C=this.B=null;this.g=[];this.g.push((0,m.O)(this.k,"click",(0,m.s)(this.Sm,this)));this.g.push((0,m.O)(this.j,"submit",(0,m.s)(this.Tm,this)));this.g.push((0,m.O)(this.da,"click",(0,m.s)(this.vi,this)));this.g.push((0,m.O)(this.A,"click",(0,m.s)(function(a){this.K||(0,m.Yh)(this.V,a.target)||cW(this)},this)));this.g.push((0,m.O)(this.pa,"click",function(a){a.stopPropagation()}));
this.g.push((0,m.O)(this.b,"submit",(0,m.s)(this.Um,this)));this.g.push((0,m.O)(this.oa,"click",(0,m.s)(function(a){a.stopPropagation();dW(this,!0)},this)));this.N&&this.g.push((0,m.O)(this.N,"click",(0,m.s)(function(a){a.stopPropagation();cW(this)},this)))};var dW=function(a,b){(0,m.L)(a.fa);(0,m.S)(a.b);(0,m.E)(a.A,"watch-editable");b&&(a.B&&(a.D.value=a.B),a.C&&((0,m.dI)(a.G,a.C),m.Jp.getInstance().Kb(a.G)));a.K=!1};
var cW=function(a){(0,m.S)(a.fa);(0,m.L)(a.b);(0,m.F)(a.A,"watch-editable");(0,m.F)(a.b,"yt-uix-form-error");a.B=a.D.value;a.C=a.G.value;a.b.scrollIntoView();a.D.select();a.K=!0};var eW=function(a){for(var b in a){var c=(0,m.jj)(a[b]);(0,m.B)(c.childNodes,function(a){if(a.id){var b=(0,m.I)(a.id);b&&(b.innerHTML=a.innerHTML,b.className=a.className,b.title=a.title)}})}};var fW;m.f=bW.prototype;m.f.dispose=function(){(0,m.Q)(this.g);this.g=[]};
m.f.vi=function(){(0,m.S)(this.j);(0,m.L)(this.k);(0,m.E)(this.R,"watch-editable")};m.f.Sm=function(){(0,m.S)(this.k);(0,m.L)(this.j);(0,m.F)(this.R,"watch-editable");(0,m.F)(this.j,"yt-uix-form-error");var a=(0,m.J)("yt-uix-form-input-text",this.j);a.value=(0,m.cl)(this.k);a.select()};m.f.Tm=function(a){a.preventDefault();(0,m.Cm)(this.j,{context:this,onSuccess:this.Cs})};m.f.Cs=function(a,b){0<b.errors.length?((0,m.el)(this.$,b.errors[0]),(0,m.E)(this.j,"yt-uix-form-error")):(this.vi(),eW(b.html))};
m.f.Um=function(a){a.preventDefault();(0,m.Cm)(this.b,{context:this,onSuccess:this.Br})};m.f.Br=function(a,b){0<b.errors.length?((0,m.el)(this.ia,b.errors[0]),(0,m.E)(this.b,"yt-uix-form-error")):(dW(this),eW(b.html))};(0,m.wb)((0,m.Al)({name:"www/watch_edit",deps:["www/watch"],page:"watch",init:function(){!fW&&(0,m.w)("WATCH_EDIT_ENABLED")&&(fW=new bW)},dispose:function(){fW&&(fW.dispose(),fW=null)}}));})(_yt_www);