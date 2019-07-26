angular.module("APP", ['ui.router'])

    .config(function ($stateProvider, $urlRouterProvider) {

        $stateProvider.state({
            name: 'index',
            url: '/',
            component: 'indexComponent',
            resolve: {
                data: function ($http) {
                    return $http.get('/api/video').then(x => x.data);
                }
            }

        })


        $stateProvider.state({
            name: 'index.about',
            url: 'about',
            component: 'aboutComponent',            
            resolve: {
                about: function ($http) {
                    return $http.get('/api').then(x => x.data);
                }
            }


        })


        $urlRouterProvider.when('', '/');

    })


    .component('indexComponent', {
        templateUrl: '/app/index.component.html',
        bindings: {data: "<"},
        controller: function ($http) {
            var $ctrl = this;
            this.$onInit = function () {
            }
        }
    })

    .component('aboutComponent', {
        templateUrl: '/app/about.component.html',
        bindings: { about: "<" },
        controller: function ($http) {
            var $ctrl = this;
            this.$onInit = function () {
            }
        }
    })


