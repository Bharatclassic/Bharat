<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="AccountHead.aspx.cs" Inherits="AccountHead" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
<link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />

<asp:Panel ID="Panel1" runat="server" Width="1000px" > 

 <fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Account Head
    </h1>
    </legend>
    
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>
      <table id="Table1" align="center">
                      <tr>
                    <td>
                        <asp:Label ID="lblsupplier" runat="server" Text="Main Head:" Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>

                        </td>
                           <td align="left">
                               <asp:TextBox ID="txtmainhead" runat="server" width="253px"></asp:TextBox>
                               
                            </td>
                             <td>
                                   
                               </td>
                          
                            <td>
                            <asp:Label ID="lblsubhead" runat="server" Text="Sub Head:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>

                            </td>
                            <td>
                            <asp:TextBox ID="txtsubhead" runat="server" Width="253px" AutoPostBack="true"></asp:TextBox>
                                 
                            </td>
                          </tr>
                          </table>
                          <br />
                          <table align="center">
                          <tr align="center" >
                          <td>
                          <asp:Label ID="lbldate" runat="server" Text="Date:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                          </td>
                          <td>
                         <asp:TextBox ID="txtdate" runat="server" AutoPostBack="true"></asp:TextBox>
                         <asp:ImageButton ID="imgCalender" CssClass="Calender" runat="server" ImageUrl="~/Images/calendar.png" />
                          <asp:CalendarExtender ID="CalendarExtender1"  runat="server" CssClass="black" Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtdate"  PopupButtonID="imgCalender">
                           </asp:CalendarExtender>
                          </td>
                          </tr>
                          </table>
                          <br />
           
                          <table align="center">
                          <tr>
                           <td>
                        <asp:Label ID="lblbankaccount" runat="server" Text="Bank Account:" Font-Size="Medium" Font-Bold="true" CssClass="Label"></asp:Label>
                         <br />
                        </td>
                       
                           <td align="left">
                                <asp:CheckBox ID="chkbankaccount" runat="server" TabIndex='2'  AutoPostBack="true"
                                    CssClass="CheckBox" onkeydown="return(event.keyCode != 13)" 
                                    oncheckedchanged="chkbankaccount_CheckedChanged"/>
                              
                            </td>
                           <td>
                               </td>
                            <td>
                            <asp:Label ID="lblothers" runat="server" Text="Others:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>

                            </td>
                            <td>
                             <asp:CheckBox ID="Chkothers" runat="server" TabIndex='2' CssClass="CheckBox" 
                                    onkeydown="return(event.keyCode != 13)"  AutoPostBack="true"
                                    oncheckedchanged="Chkothers_CheckedChanged"/>
                           
                            </td>
                          
                          
                          </tr>

                         </table>
           </fieldset>
        </asp:Panel>
        
            <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
             <asp:Button ID="btnsave" runat="server" Text="Save"  Width="95px" 
                    CssClass="Buttons embossed-link" onclick="btnsave_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" 
                    CssClass="Buttons embossed-link" onclick="btnExit_Click"/>  
                </div> 





</asp:Content>

