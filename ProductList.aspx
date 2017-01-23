<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="ProductList.aspx.cs" Inherits="ProductList" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
<link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />
        
   <script type="text/javascript">
       function show(_this) {
          document.getElementById("enlarge").innerHTML = "<img src='" + _this.src + "'+'width=216 height=216' >";
           document.getElementById("Image1").innerHTML = "";
          
       }
       function Hide(_this) {
           document.getElementById("enlarge").innerHTML = "";
       }
       function move_layer(event) {
           event = event || windows.event;
           document.getElementById("enlarge").style.left = event.clientX + document.body.scrollLeft + 10 + "px";
           document.getElementById("enlarge").style.top = event.clientY + document.body.scrollTop + 10 + "px";
       }

       $(document).ready(function () {
           imageelement = $("#image1");
           imageelement.hover(
   function () {
       $(this).attr("imageelement.width", "1000px");
       $(this).attr(imageelement.height, 1000);
   }
   );
       });
function cancelClick() {
    $find('modalbehavior').hide();
}








</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server"  > 

 <fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Product List
    </h1>
    </legend>
    
   
                           <div align="left" class="SubmitButtons" 
        style="position:relative; left: 443px; top: 2px;"> 
             <asp:Button ID="btnsave" runat="server" Text="Product List Report"  Width="203px" 
                    CssClass="Buttons embossed-link" OnClick="btnsave_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" 
                    CssClass="Buttons embossed-link" onclick="btnExit_Click" />  
                </div>
                                        
           </fieldset>
        </asp:Panel> 
</asp:Content>

