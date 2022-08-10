/// <reference path="../upass-app.js" />

angular.module('upass-app')
.controller('ProfileJSController', function ($scope, ProfileService) {

    var vm = this;
    //- get list or index
    //vm.profiles = null;
    ProfileService.getProfileList().then(function (p) {
        vm.profiles =p.data;
    }, function () {
        alert('Failed');
    });

    // Adding new profile 
    vm.newProfile = { Name: 'As' };
    vm.addProfile = function () {
        alert(vm.newProfile.Name);
    }
})
.factory('ProfileService', function ($http) {

    var fac = {};
    fac.getProfileList = function () {
        return $http.get('/Profile/GetProfiles');
    }
    return fac;
})