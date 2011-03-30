$(document).ready(function () {

    $("#Slug").change(function () {
        $("#Tag").load("GetTags", { slug: $("#Slug").val() }, function () {
            $("#Tag").change();
        });

    });

    $("#Tag").change(function () {
        $.get("GetContent",
                { slug: $("#Slug").val(), tag: $("#Tag").val() },
                function (data) {
                    $("#Title").val(data.Title);
                    $("#ContentText").val(data.ContentText);
                }
            );
    });

    // initialize
    $("#Tag").load("GetTags", { slug: $("#Slug").val() }, function () {
        $("#Tag").change();
    });
});



