/**
 * 登录
 */
var slider;

$(function() {
    var errorMsg = $('#error-msg').val();

    if (errorMsg.length) {
        layer.alert(errorMsg || '登录失败', {
            closeBtn: 0
        });
    }

    slider = $('#slider').unslider({
        speed: 500,
        delay: 3000,
        keys: true,
        dots: true,
        fluid: false
    });

    addListeners();
});

function addListeners() {
    // 登录弹窗
    $('#btn-login').on('click', function(event) {
        event.preventDefault();

        layer.open({
            type: 1,
            closeBtn: 0,
            title: false,
            shift: 2,
            area: ['470px'],
            content: $('#pupop-login'),
            end: function() {
                slider.unslider('start');
            }
        });

        // 暂停轮播
        slider.unslider('stop');
    });

    // 关闭弹窗
    $('#pupop-login').find('.btn-close').on('click', function() {
        layer.closeAll('page');
    });
}
