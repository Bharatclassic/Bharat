<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="ModifyCustomerAccount.aspx.cs" Inherits="ModifyCustomerAccount" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
     <script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
     <link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />      
   <script type="text/javascript">
                   
       function toUpper(txt) {
           document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
           return true;
       }

       function alpha(e) {
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 32 || k == 8 || (k >= 37 && k <= 40));   //k=9(keycode for tab)
       }


       function HideLabel() {
           var seconds = 3;
           setTimeout(function () {
               document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
           }, seconds * 1000);
       };   
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset class="BigFieldSet">
<legend class ="BigLegend">
<h1 style="position:relative; right:0px;">
Modify Customer Account
</h1>
</legend>
<h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>

      <table id="Table1" align="center" runat="server"  cellspacing="5">
                      <tr align="left">
                    <td align="right">
                        <asp:Label ID="lblcid" runat="server" Text="Customer ID" CssClass="Label"></asp:Label>

                        </td>
                           <td align="left">
                                <asp:TextBox ID="txtcustid" runat="server"  width="253px"  
                                    onkeyup="return toUpper(this.id)" AutoPostBack="True" 
                                    onkeypress="return alpha1(event)" MaxLength="10" 
                                    ontextchanged="txtcustid_TextChanged" TabIndex="1"></asp:TextBox>
                               
                            </td>
                            <td>
                                   <asp:Label ID="lblcode" runat="server" Text="lblcode"></asp:Label>
                               </td>
                               <td align="right">
                        <asp:Label ID="lblcname" runat="server" Text="Customer Name" CssClass="Label"></asp:Label>

                        </td>
                           <td align="left">
                               <asp:TextBox ID="txtcustname" runat="server" width="253px"  CssClass="TextBox" AutoPostBack="true"
                                   onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" 
                                   ontextchanged="txtcustname_TextChanged" TabIndex="2"></asp:TextBox>
                               
                            </td>
                          </tr>
                          <tr align="left">
                    <td align="right">
                        <asp:Label ID="lblcredit" runat="server" Text="Credit Limit" CssClass="Label"></asp:Label>

                        </td>
                           <td align="left">
                             <asp:TextBox ID="txtclimit" runat="server"  width="253px"  onkeyup="return toUpper(this.id)" AutoPostBack="True" onkeypress="return alpha1(event)" required="required" MaxLength="10" TabIndex="3"></asp:TextBox>
                               
                            </td>
                            </tr>
                             <tr align="left">
                    <td align="right">
                        <asp:Label ID="lblmail" runat="server" Text="Email ID" CssClass="Label"></asp:Label>

                        </td>
                           <td align="left">
                               <asp:TextBox ID="txtemail" runat="server" width="253px"  CssClass="TextBox"
                                 required="required"  onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" TabIndex="4"></asp:TextBox>
                              
                            </td>
                            </tr>
                             <tr>
    <td align="right">
        <asp:Label ID="lblmobile" runat="server" Text="Mobile No" Font-Bold="true" CssClass="Label"></asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtmobile" runat="server"  width="253px"  onkeyup="return toUpper(this.id)" required="required" AutoPostBack="True" onkeypress="return alpha1(event)" TabIndex="5" MaxLength="10"></asp:TextBox>
    </td>
    
    
    
    </tr>
             </table>
             <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
      <asp:Button ID="btnsave" runat="server" Text="Update" Width="95px" 
                     CssClass="Buttons embossed-link"  TabIndex="6" onclick="btnsave_Click" />
      <asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px"  
                     CssClass="Buttons embossed-link" 
                     onclick="btnexit_Click" TabIndex="7" formnovalidate="false"/>
    </div>
    </br>
</fieldset>
</asp:Content>

