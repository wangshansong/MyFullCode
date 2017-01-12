

(function ($) {
    $.extend($, {
        //procAjaxData追加在JQuery对象中，用于全局Ajax返回消息的处理
        procAjaxData: function (data, funcSuc, funcErr) {
            if (!data.Statu) {
                return;
            }

            switch (data.Statu) {
                case "ok":
                    alert("OK:" + data.Msg);
                    if (funcSuc) funcSuc(data);
                    break;
                case "err":
                    alert("ERR:" + data.Msg);
                    if (funcErr) funcErr(data);
                    break;
            }
        }
    });
}(jQuery));