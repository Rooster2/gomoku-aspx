<%@ Page Language="C#" AutoEventWireup="true" CodeFile="board.aspx.cs" Inherits="board" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>a demo</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" />
			<asp:Timer ID="Timer1" runat="server" Interval="6000"></asp:Timer>
			<asp:Button ID="buttonRestart" runat="server" onclick="Button1_Click" 
            Text="Restart" />
			<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
				<ContentTemplate>
			        <div>
		                <asp:ImageButton ID="img11" runat="server" ImageUrl="~/images/aGrid2.png" Height="128px" Width="128px" OnClick="img_Click" />
                        <asp:ImageButton ID="img12" runat="server" ImageUrl="~/images/aGrid2.png" Height="128px" Width="128px" OnClick="img_Click" />
                        <asp:ImageButton ID="img13" runat="server" ImageUrl="~/images/aGrid2.png" Height="128px" Width="128px" OnClick="img_Click" /><br />
                        <asp:ImageButton ID="img21" runat="server" ImageUrl="~/images/aGrid2.png" Height="128px" Width="128px" OnClick="img_Click" />
                        <asp:ImageButton ID="img22" runat="server" ImageUrl="~/images/aGrid2.png" Height="128px" Width="128px" OnClick="img_Click" />
                        <asp:ImageButton ID="img23" runat="server" ImageUrl="~/images/aGrid2.png" Height="128px" Width="128px" OnClick="img_Click" /><br />
                        <asp:ImageButton ID="img31" runat="server" ImageUrl="~/images/aGrid2.png" Height="128px" Width="128px" OnClick="img_Click" />
                        <asp:ImageButton ID="img32" runat="server" ImageUrl="~/images/aGrid2.png" Height="128px" Width="128px" OnClick="img_Click" />
                        <asp:ImageButton ID="img33" runat="server" ImageUrl="~/images/aGrid2.png" Height="128px" Width="128px" OnClick="img_Click" /><br />
                    </div>
				</ContentTemplate>
				<Triggers>
					<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
				</Triggers>
			</asp:UpdatePanel>
        <script runat="server">
        </script>
    </form>
</body>
</html>
