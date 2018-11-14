/// <binding BeforeBuild='all' />
module.exports = function (grunt) {
    require('load-grunt-tasks')(grunt);
    grunt.initConfig({
        clean: ["Script/Vendor/"],
        concat: {
            options: {
                stripBanners: {
                    block: true,
                    line: true
                }
            },
            vendorScripts: {
                src: [
                    'node_modules/jquery/dist/jquery.min.js',
                    'node_modules/knockout/build/output/knockout-latest.js',
                    //'node_modules/bootstrap/dist/js/bootstrap.min.js',
                    'node_modules/toastr/build/toastr.min.js',
                    'node_modules/finchjs/finch.min.js',
                    'Sources/jquery-easyui-1.6.7/jquery.easyui.min.js',
                    'Sources/jquery-easyui-ribbon/jquery.ribbon.js',
                ],
                dest: 'Script/Vendor/vendor.js'
            },
            vendorCss: {
                src: [
                    //'node_modules/bootstrap/dist/css/bootstrap.css',
                    'node_modules/toastr/build/toastr.min.css',
                    'Sources/jquery-easyui-1.6.7/themes/default/easyui.css',
                    'Sources/jquery-easyui-1.6.7/themes/icon.css',
                    'Sources/jquery-easyui-ribbon/ribbon.css',
                    'Sources/jquery-easyui-ribbon/ribbon-icon.css',
                ],
                dest: 'css/vendor.css'
            }
        }
    });

    grunt.registerTask("all", ['clean', 'concat']);
};