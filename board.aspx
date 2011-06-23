<%@ Page Language="C#" AutoEventWireup="true" CodeFile="board.aspx.cs" Inherits="board" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="style.css" rel="stylesheet" type="text/css" />
    <title>Chessboard</title>
    </head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" />
		<asp:Timer ID="TimerBoard" runat="server" Interval="800"></asp:Timer>
        <asp:Timer ID="TimerPersons" runat="server" Interval="3000"></asp:Timer>

        <asp:Label ID="labelWelcome" runat="server" Text="Welcome: "></asp:Label>
        <asp:Literal runat="server" > | </asp:Literal>
        <asp:LinkButton ID="linkBackToBoardList" Text="back to the Board List" runat="server" 
            onclick="linkBackToBoardList_Click" />
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                White:&nbsp;<asp:PlaceHolder ID="WhiteSideHolder" runat="server"></asp:PlaceHolder>
                <br />
                Black:&nbsp;<asp:PlaceHolder ID="BlackSideHolder" runat="server"></asp:PlaceHolder>
                <br />
                <asp:PlaceHolder ID="WinnerIs" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br />

        <table>
            <tr>
                <td></td>
                <td>
                    <table>
                        <tr>
                            <td class="border-ul"></td>
                            <td class="border-upper"></td>
                            <td class="border-ur"></td>
                        </tr>
                        <tr>
                            <td class="border-left"></td>
                            <td class="board">
                                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
			                        <ContentTemplate>
                                        <asp:PlaceHolder ID="ChessboardHolder" runat="server" />
                                    </ContentTemplate>
			                        <Triggers>
				                        <asp:AsyncPostBackTrigger ControlID="TimerBoard" EventName="Tick" />
			                        </Triggers>
		                        </asp:UpdatePanel>
                            </td>
                            <td class="border-right"></td>
                        </tr>
                        <tr>
                            <td class="border-ll"></td>
                            <td class="border-lower"></td>
                            <td class="border-lr"></td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
			            <ContentTemplate>
                            <!--
                            <asp:Literal Text="Players" runat="server" />
                            <asp:BulletedList ID="ListPlayers" runat="server" />
                            <br />
                            -->
                            <asp:Literal Text="Persons" runat="server" />
                            <asp:BulletedList ID="ListViewers" runat="server" />
                        </ContentTemplate>
			            <Triggers>
				            <asp:AsyncPostBackTrigger ControlID="TimerPersons" EventName="Tick" />
			            </Triggers>
		            </asp:UpdatePanel>
                    <br />
                    <br /> 
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
		        </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />

    </form>
</body>
</html>
