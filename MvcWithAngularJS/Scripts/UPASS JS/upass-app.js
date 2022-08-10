/// <reference path="../angular.js" />

(function () {
    // create a module
    var app = angular.module('upass-app', ['ngRoute']);


    //create a cntroller
    app.controller('HomeController',function ($scope){
        $scope.Message = "This is test!!";
    });
})();
