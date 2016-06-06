var gulp = require('gulp');
var less = require('gulp-less');
var path = require('path');
var minifyCSS = require('gulp-minify-css');
var LessPluginCleanCSS = require('less-plugin-clean-css');
var sourcemaps = require('gulp-sourcemaps');
var uglify = require('gulp-uglifyjs');
var watchLess = require('gulp-watch-less');

// **********************************

gulp.task('less', function () {
    return gulp.src('css/*.less')
        .pipe(less({
            paths: [path.join(__dirname, 'less', 'includes')]
        }))
        .pipe(minifyCSS())
        .pipe(gulp.dest('css/dest/'));
});

gulp.task('default', function () {
    gulp.run('less');
});

gulp.task('uglify', function () {
    gulp.src(['Templates/**/*.js'])
      .pipe(uglify('app-all.js', {
          mangle: false,
          output: {
              beautify: true
          }
      }))
      .pipe(gulp.dest('scripts/dist'))
});

gulp.task('less', function () {
    return gulp.src('Content/Less/**/*.less')
        .pipe(less({
            paths: [path.join(__dirname, 'less', 'includes')]
        }))
        .pipe(minifyCSS())
        .pipe(gulp.dest('Content/Css/'));
});

gulp.task('watch-less', function () {
    return gulp.src('Content/Less/**/*.less')
        .pipe(watchLess('Content/Less/**/*.less'))
        .pipe(less())
        .pipe(gulp.dest('dist'));
});
