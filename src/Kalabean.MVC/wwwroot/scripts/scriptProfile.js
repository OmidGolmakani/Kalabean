
$(document).ready(function () {

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })

    /*****************  input image  *******************/
    $(document).on("click", ".browse", function () {
        var file = $(this).parents().find(".input-file");
        file.trigger("click");
    });
    $('input[type="file"]').change(function (e) {
        var fileName = e.target.files[0].name;
        $("#FileInput").val(fileName);

        var reader = new FileReader();
        reader.onload = function (e) {
            // get loaded data and render thumbnail.
            document.getElementById("preview").src = e.target.result;
        };
        // read the image file as a data URL.
        reader.readAsDataURL(this.files[0]);
    });
    /*************************************************/

});