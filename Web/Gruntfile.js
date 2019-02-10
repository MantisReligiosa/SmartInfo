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
                    'node_modules/popper.js/dist/umd/popper.js',
                    'node_modules/bootstrap/dist/js/bootstrap.js',
                    'node_modules/toastr/build/toastr.min.js',
                    'node_modules/interact.js/dist/interact.min.js',
                    'node_modules/bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js'
                ],
                dest: 'Script/Vendor/vendor.js'
            },
            vendorCss: {
                src: [
                    'node_modules/bootstrap/dist/css/bootstrap.css',
                    'node_modules/toastr/build/toastr.min.css',
                    'node_modules/bootstrap-colorpicker/dist/css/bootstrap-colorpicker.min.css'
                ],
                dest: 'css/vendor.css'
            }
        }
    });

    grunt.registerTask("all", ['clean', 'concat']);
};