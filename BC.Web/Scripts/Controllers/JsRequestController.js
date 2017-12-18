function RequestController(){ }

RequestController.prototype = {
    GetRequestsByProjectId: function (url, div, data)
    {
        let MyController = this;

        if (data.pId === '') alert("Please, inform the project Id");
        else if (/\D/.test(data.pId)) alert("Please, inform just numbers in the project Id");
        else {
            let jsController = new JsController(url, div, data, "GET", true);
            jsController.callPartialView(response => {
                div.html(response);

                $("#modalRequest").on('show.bs.modal', function (event) {
                    let mode = $(event.relatedTarget).data('mode');
                    let id = $(event.relatedTarget).data('id');

                    let modal = $(this);
                    modal.find('.modal-title').text(mode + ' a Request');

                    MyController.ClearModal();
                    MyController.GetRequestById(id);

                    let urlToSaveUpdate = "/Request/" + mode + "Request";
                    
                    $("#btnSaveUpdateRequest").off();
                    $("#btnSaveUpdateRequest").on('click', function () {
                        MyController.SaveUpdateRequest(urlToSaveUpdate, div, MyController.GetAllDataFromModal());
                    });
                });
            });
        }
    },
    GetRequestById: function (id) {
        if (id != undefined) {
            $.get("/Request/GetRequestByID", { Id: id })
                .done(response => {
                    if (response != null) {
                        $("#txtId").val(response.Id);
                        $("#txtValue").val(response.Value);
                        $("#txtIdSupplier").val(response.IdSupplier);
                    }
                });
        }
    },
    SaveUpdateRequest: function (url, div, data)
    {
        this.ValidateData(data, response => {
            if (response.ret) {
                let jsController = new JsController(url, div, data, "POST", false);
                jsController.callPartialView(response => {
                    div.html(response);
                    $('#modalRequest').modal('hide');
                    alert("Request saved!");
                });
            } else {
                alert(response.msg);
            }
        });
    },
    ClearModal: function () {
        $("#txtId").val("");
        $("#txtValue").val("");
        $("#txtIdSupplier").val("");
    },
    ValidateData: function (data, callback) {
        let msg = "";
        data.Id = data.Id === "" ? null : data.Id;
        if (data.Value === "") msg += "Please, inform the request's value.\n";
        if (data.IdSupplier === "") msg += "Please, inform the request's supplier.\n";
        callback({ ret: msg === "" ? true : false, msg: msg });
    },
    GetAllDataFromModal: function () {
        var json = {
            __RequestVerificationToken:$('input[name="__RequestVerificationToken"]').val(),
            Id: $("#txtId").val(),
            IdProject: $("#txtIdProject").val(),
            Value: $("#txtValue").val(),
            IdSupplier: $("#txtIdSupplier").val()
        };
        return json;
    }
};

window.onload = function () {
    $("#btnSearch").click(function () {
        new RequestController().GetRequestsByProjectId("/Request/SearchProject", $("#listOfRequests"), { pId: $("#pId").val() });
    });
}