<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TaskList>>" %>
<%--
<html xmlns="http://www.w3.org/1999/xhtml"> 
--%>
<html>
<head runat="server">
	<title></title>
</head>
<body>
	<h1>TaskLists</h1>
	<div>
	<table id="resultTable">
		<% foreach (var taskList in ViewData.Model) { %>
		<tr>
			<td><%=taskList.Id.ToString()%></td>
			<td><%=taskList.Name%></td>
		</tr>
		<% } %>
	</table>
	<table></table>
	</div>
</body>
</html>
