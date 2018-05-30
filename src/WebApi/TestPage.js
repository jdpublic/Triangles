
function drawTriangle(triangle) {

    console.log(triangle);
    console.log(triangle.Vertices.length);

    var c = $("#canvas1")[0];
    var ctx = c.getContext("2d");
    var p1 = triangle.Vertices[0];
    var p2 = triangle.Vertices[1];
    var p3 = triangle.Vertices[2];

    console.log(c);
    console.log(p1);
    console.log(p2);
    console.log(p3);

    var zoom = 2; //simple zoom here could look at canvas scale method also
    ctx.moveTo(p1.X * zoom, -p1.Y * zoom);
    ctx.lineTo(p2.X * zoom, -p2.Y * zoom);
    ctx.lineTo(p3.X * zoom, -p3.Y * zoom);
    ctx.lineTo(p1.X * zoom, -p1.Y * zoom);
    ctx.stroke();

}

function getTriangleCallback(result, status, xhr) {
    //alert(status);
    var jqEl = $("#content1");
    jqEl.append("timestamp = " + (new Date()).toTimeString() + " <br/>");
    jqEl.append("status = " + status + " <br/>");
    jqEl.append("json = " + JSON.stringify(result));
    drawTriangle(result);
}

function btnClickHandler(e) {
    try {

        var gridRef = $("#txtGridRef").val().trim();

        var row = gridRef.substr(0, 1); //"A";
        var col = gridRef.substr(1).trim(); //"1";

        var url = "api/imagegrid/GetTriangle?row=" + row + "&col=" + col;

        $("#content1").empty();
        $("#contentError").empty();
        $.getJSON(url, getTriangleCallback)
            .fail(function (jqxhr, status, error) { handleError(error); });

    } catch (e) {
        handleError(e);
    }
}

function handleError(e) {
    $("#contentError").empty();
    $("#contentError").append("ERROR: " + e + "<br/>");
}

