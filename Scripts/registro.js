var urlObtenerSaldo = "/Home/ObtenerSaldoAnterior";

$(document).ready(function () {
    $("#ddlCompra").on("change", function () {
        var idCompra = $(this).val();

        if (!idCompra) {
            $("#txtSaldoAnterior").val("0.00");
            $("#txtAbono").val("");
            return;
        }

        $.ajax({
            url: urlObtenerSaldo,
            type: "GET",
            data: { idCompra: idCompra },
            success: function (respuesta) {
                if (respuesta.ok) {
                    $("#txtSaldoAnterior").val(parseFloat(respuesta.saldo).toFixed(2));
                    $("#txtAbono").val("");
                    $("#mensajeValidacion").hide();
                } else {
                    $("#txtSaldoAnterior").val("0.00");
                }
            },
            error: function () {
                $("#txtSaldoAnterior").val("0.00");
            }
        });
    });

    $("#btnAbonar").on("click", function (e) {
        var saldo = parseFloat($("#txtSaldoAnterior").val()) || 0;
        var abono = parseFloat($("#txtAbono").val()) || 0;

        if (abono > saldo) {
            e.preventDefault();
            $("#mensajeValidacion").text("El abono no puede ser mayor al saldo anterior.").show();
            return false;
        }

        $("#mensajeValidacion").hide();
    });
});
