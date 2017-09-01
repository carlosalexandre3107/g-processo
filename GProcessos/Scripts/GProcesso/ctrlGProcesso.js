var app = angular.module('gProcesso', []);

app.controller('ctrlGProcesso', ['$scope', 'BackEndServico', function ($scope, BackEndServico) {

    BackEndServico.Post("GProcesso/EstruturaProcesso")
    .then(function (_processo) {
        _processo.DataInicio = new Date();
        $scope.Processo = _processo;
     }, function (data) {
        console.log("Erro ao criando estrutura do processo");
     });

    BackEndServico.Post("GProcesso/ListarProcessos")
    .then(function (_processos) {
        $scope.Processos = _processos;
    }, function (data) {
        console.log("Erro listar processos");
    });

    $scope.SalvarProcesso = function () {
        console.log($scope.Processo);
        BackEndServico.Post("GProcesso/SalvarProcesso", $scope.Processo)
        .then(function (_processos) {
            $scope.Processos = _processos;
        }, function (data) {
            console.log("Erro ao retornar processos");
        });
    }

}]);

//SERVIÇO
app.factory('BackEndServico', ['$http', '$q', '$location', function ($http, $q, $location) {

    var BackEndServico = { Post: Post }
    return BackEndServico;

    function Post(caminho, objetoDTO) {
        var _url = "http://" + $location.host() + ":" + $location.port() + "/" + caminho;
        var def = $q.defer();
        if (objetoDTO != undefined) { var result = $http.post(_url, objetoDTO) } else { var result = $http.post(_url) }
        result.success(function (data) {
            def.resolve(data);
        }).error(function () {
            def.reject("Erro no caminho " + caminho);
        });

        return def.promise;
    }

}]);

//FILTRO DE CONVERSÃO DE DATA
app.filter('filterConverteShortDataJson', function () {
    return function (text, length, end) {

        var dataJavaScript = "";
        var shortDate = "";
        if (text != undefined) {
            if (text.length > 0) {
                dataJavaScript = new Date(parseInt(text.substr(6)));
                shortDate = ('0' + dataJavaScript.getDate()).slice(-2) + "/" + ('0' + dataJavaScript.getMonth()).slice(-2) + "/" + dataJavaScript.getFullYear();
            };
        };
        return shortDate;
    }

});

//FILTRO BOOLEAN
app.filter('filterBoolean', function () {
    return function (text) {
        return text ? "Sim" : "Não";
    }

});

//CONVERTE /Date(1466030400000)/ PARA Wed Jun 15 2016 19:00:00 GMT-0300 (Hora oficial do Brasil) INPUT
app.filter('ConverteDataJsonParaDataJS', function () {
    return function (text, length, end) {

        var dataJavaScript = "";
        var DateJson = text;
        if (DateJson != undefined) {
            if (DateJson.length > 0) {
                if (DateJson.substr(0, 6) == "/Date(") {
                    dataJavaScript = new Date(parseInt(DateJson.substr(6)));
                }
                else {
                    dataJavaScript = DateJson;
                }
            };
        };
        return dataJavaScript;
    }

});