<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<%--<meta name="viewport" content="width=device-width, initial-scale=1">--%>
<head runat="server">
    <title>Pharmacy Software</title>
    <link rel="Stylesheet" type="text/css" href="Styles/Main.css" />
    <link rel="icon" type="image/x-icon" href="Temple_of_Neptune_(building)-icon" />
    <script type="text/javascript" src="JavaScripts/jquery-1.9.0.min.js"></script>
     <script type="text/javascript" src="JavaScripts/jquery.nivo.slider.js"></script>
     <script type="text/javascript">
        $(window).load(function()
        {
        $('#slider').nivoSlider();
        });</script>
</head>
<body onload="disableBackButton()">
   <form id="form1" runat="server" >
   <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div class="Header">
        <div class="HeaderContent">
            <div class="Logo">
            </div>
        </div>
    </div>
     
         <br />
         <br />
         
         <table align="Left" style="width: 343px; height: 314px">
         <tr>
         <td>
       <div class="Header1">
        <div class="HeaderContent">
            <div class="Logo">
            </div>
        </div>
    </div>  
     </td>
         </tr>
         </table>
         <asp:Panel ID = "tblpanel" runat="server" >
          <table id="Table2" cellpadding="10" runat="server" style="border: solid 15px Green;background-color:SkyBlue"  cellspacing="10"  width="50%" align="center">
      <tr>
            <td align="center">
                <span style="color: Red; font-weight: bold; font-size: 18pt;"></span>&nbsp;
                <asp:Label ID="lblmod" runat="server" Text="lblmodsuccess" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
                </td>
                </tr>
                <tr>
                <td>
                <asp:Button ID="btn" runat="server" Width="95px" Text ="Yes" 
                    CssClass="Buttons embossed-link" onclick="btn_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnmodify" runat="server" Width="95px" Text ="No" 
                    CssClass="Buttons embossed-link" onclick="btnmodify_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;
                
               
            </td>
        </tr>
    </table>
    </asp:Panel>
         <table align="center">
        <!--  <tr>
        <td>
         <asp:Image ID="Image1" Visible = "false"  runat="server" Height = "50" Width = "50" />
         </td>
         </tr>-->
         </table>
         
         <div class="LoginPanel" >
                    <div class="LoginPanelHeader" >
                    <h1 >
                            Login</h1>
                    </div>
                        <table align="center" runat="server">
                            <tr>
                                <td>
                                    <asp:Label ID="lblUserName" runat="server" Text="Username" CssClass="Label"></asp:Label>
                                </td>
                                <td class="Label">
                                    :
                                </td>
                                <td>
                               
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="LoginTextBox" AutoPostBack="true"
                                        ontextchanged="txtUserName_TextChanged"></asp:TextBox>
                                      
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="Label"></asp:Label>
                                </td>
                                <td class="Label">
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="PasswordTextBox" 
                                        TextMode="Password" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="Message" id="Message" runat="server">
                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="AliceBlue" BackColor="Red" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="LoginBtns">
                            <asp:LinkButton ID="lnkBtnForgot" runat="server" CssClass="LinkButton">Forgot Password ?</asp:LinkButton>
                            <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="Buttons"
                                Width="307" Height="31" onclick="btnLogin_Click"  />
                        </div>
                    </div>
                
    <br />
         <br />
         <br />
         <br />
    <div class="Clear">
    </div>  
    <div class="footer">
        <div class="footerContent">
            <p style="float: left;">
                Copyright &copy; Pharmacy Software</p>
            <p style="float: right;">
                Powered by <a href="http://www.vagiindia.com" target="_blank">Vagi Data Systems</a>
            </p>
        </div>
    </div>
        <asp:UpdatePanel ID="Panel1" runat="server" Style="display: none;">
        <ContentTemplate>
        <div align="center" class="ConfirmBoxHeader">
            <asp:Label ID="Label1" runat="server" Text="! Error" CssClass="Label" align="center"></asp:Label>
        </div>
        <div class="Clear">
        </div>
        <div class="ConfirmBox">
            <div align="center" style="width: 380px; height: 62px; margin: 0 auto;">
                <div style="float: left;">
                    <img src="Images/1389184475_Error.png" alt="" />
                </div>
                <div style="float: left; width: 300px; text-align: center" align="center">
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="Label" ForeColor="Red" align="center"></asp:Label>
                </div>
            </div>
            <div class="Clear">
            </div>
            <div align="center">
                <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="Buttons embossed-link"
                  OnClick="btnOK_Click"/>
                  <asp:Button ID="btnNo" runat="server" Text="No" CssClass="Buttons embossed-link" OnClick="btnNo_Click" />
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
           <asp:HiddenField ID="HiddenField" runat="server" />
    <asp:ModalPopupExtender ID="ErrorMessage" runat="server" PopupControlID="Panel1"
        TargetControlID="HiddenField" BackgroundCssClass="modal">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.4" Fps="40"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    </form>
</body>
</html>
