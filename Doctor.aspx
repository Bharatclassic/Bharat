<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Doctor.aspx.cs" Inherits="Doctor" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
        
   <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
<link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />

        
   <script type="text/javascript">
      

       function alpha(e) {
           var k = e.charCode ? e.charCode : e.keyCode;
           return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab)
       }

       

       function toUpper(txt) {
           document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
           return true;
       }
      
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
     Doctor
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


                
                <asp:Button ID="btn" runat="server" Width="95px" Text ="No" CssClass="Buttons embossed-link" onclick="btn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
              <%-- <asp:Label ID="lblmodify" runat="server" Text="Modify" Font-Bold="true" Font-Size="Large" ForeColor="Black"></asp:Label>&nbsp;&nbsp; <asp:CheckBox ID="chkmodify" runat="server" OnCheckedChanged="chkmodify_CheckedChanged"  />--%>
                <asp:Button ID="btnmodify" runat="server" Width="95px" CssClass="Buttons embossed-link" Text ="Modify" 
                        onclick="btnmodify_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btndelete" runat="server" Width="95px" Text ="Delete" />

               
               
            </td>
        </tr>
    </table>
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>
             <table id="Table1" align="center" runat="server" width="520px" cellspacing="5">
                      <tr>
                      <td align="right" class="auto-style1">
                          <asp:Label ID="lbldname" runat="server" Text="Doctor Name:" CssClass="Label"  onkeypress="return alpha(event)"></asp:Label>
                           <asp:Label ID="lbldr" runat="server" Text="Dr." CssClass="Label"  onkeypress="return alpha(event)"></asp:Label>
                          </td>
                           <td align="left" class="auto-style1">
                              <asp:TextBox ID="txtdname" width="253px" runat="server" autopostback="true" TabIndex="1" 
                                   onkeypress="return alpha(event)" OnTextChanged="txtdname_TextChanged" onkeyup="return toUpper(this.id)" />
                                
                               <%-- <asp:HiddenField ID="hfCustomerId" runat="server" />--%>
                            <asp:AutoCompleteExtender ID="txtdname_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                               Enabled="True" ServiceMethod="Buyername" ServicePath="" TargetControlID="txtdname" CompletionListItemCssClass="OtherCompletionItemCssClass" CompletionInterval="10"
                               UseContextKey="True" MinimumPrefixLength="1" CompletionListCssClass="OtherCompletionCssClass"  CompletionListHighlightedItemCssClass="OtherCompletionHighlightedCssClass">
                           </asp:AutoCompleteExtender>
                               </td>
                               <td>
                                   <asp:Label ID="lblcode" runat="server" Text="lblcode"></asp:Label>
                                   <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                               </td>
                          </tr>
                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lbldspec" runat="server" Text="Doctor Specialisation:" CssClass="Label"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdspec" runat="server" width="253px" onkeydown="return(event.keyCode != 13)" TabIndex="2" onkeypress="return alpha(event)" onkeyup="return toUpper(this.id)" CssClass="TextBox"></asp:TextBox>
                            <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                        </td>

                        
                    </tr>
                   </table>
                   <div style="width: 980px; margin: 0 auto; padding: 0" align="center">
           <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
            <asp:GridView ID="Griddoctor" runat="server"  Width="629px" CellPadding="4" BackColor="Yellow"
             AllowPaging="true"  Font-Bold="true" ForeColor="Black" Font-Names="Times New Roman" Font-Size="Small"
                      GridLines="Both" OnPageIndexChanging="Griddoctor_PageIndexChanging" PageSize="5">
         <PagerStyle BackColor="Yellow" ForeColor="Black" HorizontalAlign="Center" Font-Size="12px"/>
         </asp:GridView>
           </asp:Panel></div>



                   <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
               <asp:Button ID="btnsave" runat="server" Text="Save" Width="95px"  CssClass="Buttons embossed-link" OnClick="btnsave_Click" TabIndex="3" />
                             <asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px"  CssClass="Buttons embossed-link" OnClick="btnexit_Click" TabIndex="4"/>
                </div>
                       
                  
            </fieldset>
</asp:Content>




