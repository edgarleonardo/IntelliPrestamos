/// Funcion Utilizada para obtener la visualizacion de los editables de configuración para cargas FTP
function showModalConfig(element, Rol_id, partialView) {
    
    $.ajax({
        url: $(element).data("url"),
        type: 'GET',        
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: { Id_Rol: Rol_id, partial_view: partialView },
        success: function (response) {            
            $('#content-modal_info').html(response);
            $('#modalDetail').modal();
        },
        error: function () {
            alert("error");
        }
    });
    
}
function create_loan(element, sol_id,partialView) {

    $.ajax({
        url: $(element).data("url"),
        type: 'POST',
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: { solicitud_id: sol_id, view: partialView },
        success: function (response) {
            $('#content-ajax').html(response);
            $(element).hide();
        },
        error: function () {
            alert("error");
        }
    });

}
/// Funcion Utilizada para obtener la visualizacion de los editables de configuración para cargas FTP
function PutPayments(element, errorComponent,tableId, id_prestamo, numero_cuotas, monto_Cuota, monto_pago_cuota, monto_pagado_cuota, monto_mora, mora_status) {
    
    $.ajax({
        url: $(element).data("url"),
        type: 'POST',
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: { id_prestamo: id_prestamo, numero_cuotas: numero_cuotas, monto: monto_pago_cuota, mora_status: mora_status },
        success: function (response) {
            
            if (response == '')
            {
                response = 'Datos Guardados Exitosamente.';
                var valor_total_cuota = (parseFloat(monto_pago_cuota) + parseFloat(monto_pagado_cuota));
                if (mora_status == '13')
                {
                    try {
                        valor_total_cuota = valor_total_cuota + parseFloat(monto_mora);
                    } catch (err) { }                    
                }
                if (parseFloat(monto_Cuota) <= valor_total_cuota) {
                    $('#'+tableId).hide();                
                }
                else {
                    $('#Pay' + String(numero_cuotas)).html(parseFloat(monto_pago_cuota) + parseFloat(monto_pagado_cuota));
                    $('#Hidden' + String(numero_cuotas)).val(parseFloat(monto_pago_cuota) + parseFloat(monto_pagado_cuota));
                }
            }
            alert(response);
            $('#' + errorComponent).html(response);
            
        },
        error: function () {
            alert("error");
        }
    });
}

/// Funcion Utilizada para obtener la visualizacion de los editables de configuración para cargas FTP
function saveModalConfig(element, nameResultObject) {

    $.ajax({
        url: $(element).data("url"),
        type: 'GET',
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: $("#AddConfigUpdate").serialize(),
        success: function (response) {
            $('#' + nameResultObject).html(response);
            //$(element).attr("disabled", "true");
            $(element).hide();
        },
        error: function () {
            alert("error");
        }
    });

}
/// Funcion Utilizada para obtener la visualizacion de los editables de configuración para cargas FTP
function saveModalConfigHtmlEditor(element, nameResultObject) {
    
    $('#HtmlContent').val($('.note-editable').html());
    $.ajax({
        url: $(element).data("url"),
        type: 'POST',
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: $("#AddConfigUpdate").serialize(),
        success: function (response) {
            $('#' + nameResultObject).html(response);
            //$(element).attr("disabled", "true");
            $(element).hide();
        },
        error: function () {
            alert("error");
        }
    });

}

function showModalReportes(element, Cedula,Prestamo_Id,Accionista,FechaInicio,FechaFin, partialView) {
   
    $.ajax({
        url: $(element).data("url"),
        type: 'GET',
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: { cedula: Cedula, prestamo_id: Prestamo_Id, accionista: Accionista, fechaInicio: FechaInicio, fechaFin: FechaFin, view: partialView },
        success: function (response) {
            $('#content-ajax').html(response);
        },
        error: function () {
            alert("error");
        }
    });

}
/// Funcion Utilizada para obtener la visualizacion de los editables de configuración para cargas FTP
function showRefreshViews(element,  partialView) {

    $.ajax({
        url: $(element).data("url"),
        type: 'GET',
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: { partial_view: partialView },
        success: function (response) {
            $('#content-ajax').html(response);
        },
        error: function () {
            alert("error");
        }
    });

}

/// Funcion Utilizada para eliminar los registros que se desen eliminar
function delModalConfigParam(element, nameResultObject, up_id) {

    $.ajax({
        url: $(element).data("url"),
        type: 'GET',
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: { id_solicitud: up_id },
        success: function (response) {
            if (response == 'true') {
                $('#' + nameResultObject).html('Datos Eliminados Satisfactoriamente');
                //$(element).attr("disabled", "true");
                $(element).hide();
            } else {
                $('#' + nameResultObject).html('No se ha podido eliminar el registro...');
            }
        },
        error: function () {
            alert("error");
        }
    });

}

function GetInversorAmount( id) {
    var url = $("#UrlDrpItems").data("url");
    $.getJSON(url, { cedula: id },
       function (results) {
           $("#Monto_Accionista").val(results.Monto_Balance);
       });
}

function showModalSearch(element, Cedula, Prestamo_Id, Solicitud_Id, accionista, partialView) {

    $.ajax({
        url: $(element).data("url"),
        type: 'GET',
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: { cedula: Cedula, solicitud_id: Solicitud_Id, prestamo_id: Prestamo_Id, accionista: accionista,partial_view: partialView },
        success: function (response) {
            $('#content-modal_info').html('');
            $('#content-ajax').html(response);
            
        },
        error: function () {
            alert("error");
        }
    });
}

function showModalSearchSingle(element, Cedula, partialView) {

    $.ajax({
        url: $(element).data("url"),
        type: 'GET',
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: { Cedula: Cedula, partial_view: partialView },
        success: function (response) {
            $('#content-ajax').html(response);
        },
        error: function () {
            alert("error");
        }
    });
}

function GetAppliant(element, cedulaData)
{
    $.ajax({
        url: $(element).data("url"),
        type: 'POST',
        dataType: "json",
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: { cedula: cedulaData },
        success: function (response) {
            $('[name="solicitante.Nombre"]').val(response.solicitante.Nombre);
            $('[name="solicitante.Apellidos"]').val(response.solicitante.Apellidos);
            $('[name="solicitante.Direccion"]').val(response.solicitante.Direccion);
            $('[name="solicitante.Telefono"]').val(response.solicitante.Telefono);
            $('[name="solicitante.Celular"]').val(response.solicitante.Celular);
            $('[name="solicitante.Sector"]').val(response.solicitante.Sector);
            $('[name="solicitante.Email"]').val(response.solicitante.Email);
            $('[name="solicitante.Opcional"]').val(response.solicitante.Opcional);

            $('[name="Fuente_Ingreso.Nombre_Trabajo"]').val(response.Fuente_Ingreso.Nombre_Trabajo);
            $('[name="Fuente_Ingreso.Direccion"]').val(response.Fuente_Ingreso.Direccion);
            $('[name="Fuente_Ingreso.Cargo"]').val(response.Fuente_Ingreso.Cargo);
            $('[name="Fuente_Ingreso.telefono"]').val(response.Fuente_Ingreso.telefono);
            $('[name="Fuente_Ingreso.flota"]').val(response.Fuente_Ingreso.flota);
            $('[name="Fuente_Ingreso.Sueldo_Actual"]').val(response.Fuente_Ingreso.Sueldo_Actual);
            $('[name="Fuente_Ingreso.Sueldo_Actual"]').val(response.Fuente_Ingreso.ID_Banco);
            $('[name="Fuente_Ingreso.Sueldo_Actual"]').val(response.Fuente_Ingreso.Tiene_Int_Banking);
            $('[name="Fuente_Ingreso.Usuario"]').val(response.Fuente_Ingreso.Usuario);
            $('[name="Fuente_Ingreso.Clave"]').val(response.Fuente_Ingreso.Clave);
            $('[name="Fuente_Ingreso.Fecha_Ingreso"]').val(response.Fuente_Ingreso.Fecha_Ingreso);
            var dataInfo = response.referencias_Personales;
            for (i in dataInfo) {
                var stringId = String(i);
                $('[name="referencias_Personales[' + stringId + '].Nombre"]').val(dataInfo[i].Nombre);
                $('[name="referencias_Personales[' + stringId + '].Apellido"]').val(dataInfo[i].Apellido);
                $('[name="referencias_Personales[' + stringId + '].Telefono"]').val(dataInfo[i].Telefono);
                $('[name="referencias_Personales[' + stringId + '].Tipo_Referencia_Id"]').val(dataInfo[i].Tipo_Referencia_Id);
                $('[name="referencias_Personales[' + stringId + '].Version_No"]').val(dataInfo[i].Version_No);
            }

        },
        error: function () {
            alert("error");
        }
    });
}

/// Funcion Utilizada para obtener la visualizacion de los editables de configuración para cargas FTP
function PutPaymentsExtra(element, errorComponent, id_prestamo,  monto_pago_cuota, afecta_prestamo) {

    $.ajax({
        url: $(element).data("url"),
        type: 'POST',
        // we set cache: false because GET requests are often cached by browsers
        // IE is particularly aggressive in that respect
        cache: false,
        data: { id_prestamo: id_prestamo, monto: monto_pago_cuota, afecta_prestamos: afecta_prestamo },
        success: function (response) {

            if (response == '') {
                response = 'Datos Guardados Exitosamente.';                
            }
            alert(response);
            $('#' + errorComponent).html(response);

        },
        error: function () {
            alert("error");
        }
    });
}