﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Generic.aspx.cs" Inherits="Generic" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
         
   <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
   <script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
    <link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />

 <script type="text/javascript">
              $(function () {
                  $("[id$=txtgenename]").autocomplete({
                      source: function (request, response) {
                          $.ajax({
                              url: '<%=ResolveUrl("~/Generic.aspx/GetCustomers") %>',
                       data: "{ 'prefix': '" + request.term + "'}",
                       dataType: "json",
                       type: "POST",
                       contentType: "application/json; charset=utf-8",
                       success: function (data) {
                           response($.map(data.d, function (item) {
                               return {
                                   label: item.split('-')[0],
                                   val: item.split('-')[1]
                               }
                           }))
                       },
                       error: function (response) {
                           alert(response.responseText);
                       },
                       failure: function (response) {
                           alert(response.responseText);
                       }
                   });
               },
               select: function (e, i) {
                   $("[id$=hfCustomerId]").val(i.item.val);
               },
               minLength: 1
           });
       });

       function alpha(e) {
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab)
       }
       function toUpper(txt) {
           document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
           return true;
       }
       

//             
//       function onlyAlphabets(e, t) {
//           try {
//               if (window.event) {
//                   var charCode = window.event.keyCode;
//               }
//               else if (e) {
//                   var charCode = e.which;
//               }
//               else { return true; }
//               if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode == 32) || (charCode == 08))
//                   return true;
//               else
//                   return false;

//           }
//           catch (err) {
//               alert(err.Description);
//           }
//       }


       function HideLabel() {
           var seconds = 3;
           setTimeout(function () {
               document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
           }, seconds * 1000);
       };

  
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Generic
    </h1>
    </legend>
   <table id="Table2" cellpadding="10" runat="server" style="border: solid 15px Green; background-color: SkyBlue;"  cellspacing="10"  width="50%" align="center">
      <tr>
            <td align="center">
                <span style="color: Red; font-weight: bold; font-size: 18pt;"></span>&nbsp;
                <asp:Label ID="lblmod" runat="server" Text="lblmodsuccess" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                </tr>
                <tr>
                <td>
                <asp:Button ID="btn" runat="server" Width="95px" Text ="No" 
                    CssClass="Buttons embossed-link" onclick="btn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
              <%-- <asp:Label ID="lblmodify" runat="server" Text="Modify" Font-Bold="true" Font-Size="Large" ForeColor="Black"></asp:Label>&nbsp;&nbsp; <asp:CheckBox ID="chkmodify" runat="server" OnCheckedChanged="chkmodify_CheckedChanged"  />--%>
                <asp:Button ID="btnmodify" runat="server" Width="95px" Text ="Modify" 
                    CssClass="Buttons embossed-link" onclick="btnmodify_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btndelete" runat="server" Width="95px" Text ="Delete" />
               
            </td>
        </tr>
    </table>
    <h3>

     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
     <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>
             <table id="Table1" align="center" runat="server"  cellspacing="5">
                      <tr>
                    <td align="right">
                        <asp:Label ID="lblgenename" runat="server" Text="Generic Name:" CssClass="Label"></asp:Label>
                     </td>
                           <td align="left">
                               <asp:TextBox ID="txtgenename" runat="server" width="253px" 
                                   onkeyup="return toUpper(this.id)"   TabIndex="1" CssClass="TextBox" 
                                   onkeypress="return alpha(event)" ontextchanged="txtgenename_TextChanged"></asp:TextBox>
                                <asp:HiddenField ID="hfCustomerId" runat="server" />
                               </td>
                               <td>
                                   <asp:Label ID="lblcode" runat="server" Text="lblcode"></asp:Label>
                               </td>
                          </tr>
                
                       </table> 

             <div style="width: 980px; margin: 0 auto; padding: 0" align="center">
           <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
            <asp:GridView ID="Gridgeneric" runat="server" Width="629px" CellPadding="4" BackColor="Yellow"
             AllowPaging="true"  Font-Bold="true" ForeColor="Black" Font-Names="Times New Roman" Font-Size="Small"
                      GridLines="Both" OnPageIndexChanging="Gridgeneric_PageIndexChanging" PageSize="5">
         <PagerStyle BackColor="Yellow" ForeColor="Black" HorizontalAlign="Center" Font-Size="12px"/>
         </asp:GridView>
           </asp:Panel></div>



                <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
                <asp:Button ID="btnsave" runat="server" Text="Save" Width="95px" CssClass="Buttons embossed-link" OnClick="btnsave_Click" TabIndex="2"/>
             <asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px" CssClass="Buttons embossed-link" OnClick="btnexit_Click"  TabIndex="3"/>
                </div>             
         </fieldset>
</asp:Content>

