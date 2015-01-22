/*
This file is part of Ext JS 4.2

Copyright (c) 2011-2013 Sencha Inc

Contact:  http://www.sencha.com/contact

GNU General Public License Usage
This file may be used under the terms of the GNU General Public License version 3.0 as
published by the Free Software Foundation and appearing in the file LICENSE included in the
packaging of this file.

Please review the following information to ensure the GNU General Public License version 3.0
requirements will be met: http://www.gnu.org/copyleft/gpl.html.

If you are unsure which license is appropriate for your use, please contact the sales department
at http://www.sencha.com/contact.

Build date: 2013-03-11 22:33:40 (aed16176e68b5e8aa1433452b12805c0ad913836)
*/
/**
 * Traditional Chinese translation
 * By hata1234
 * 09 April 2007
 */
Ext.onReady(function() {
    var cm = Ext.ClassManager,
        exists = Ext.Function.bind(cm.get, cm),
        parseCodes;

    if (Ext.Updater) {
        Ext.Updater.defaults.indicatorText = '<div class="loading-indicator">讀取中...</div>';
    }

    if (Ext.Date) {
        Ext.Date.monthNames = ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"];

        Ext.Date.dayNames = ["日", "一", "二", "三", "四", "五", "六"];

        Ext.Date.formatCodes.a = "(this.getHours() < 12 ? '上午' : '下午')";
        Ext.Date.formatCodes.A = "(this.getHours() < 12 ? '上午' : '下午')";

        parseCodes = {
            g: 1,
            c: "if (/(上午)/i.test(results[{0}])) {\n"
                + "if (!h || h == 12) { h = 0; }\n"
                + "} else { if (!h || h < 12) { h = (h || 0) + 12; }}",
            s: "(上午|下午)",
            calcAtEnd: true
        };

        Ext.Date.parseCodes.a = Ext.Date.parseCodes.A = parseCodes;
    }

    if (Ext.MessageBox) {
        Ext.MessageBox.buttonText = {
            ok: "確定",
            cancel: "取消",
            yes: "是",
            no: "否"
        };
    }

    if (exists('Ext.util.Format')) {
        Ext.apply(Ext.util.Format, {
            thousandSeparator: ',',
            decimalSeparator: '.',
            currencySign: '\u00a5',
            // Chinese Yuan
            dateFormat: 'Y/m/d'
        });
    }

    if (exists('Ext.form.field.VTypes')) {
        Ext.apply(Ext.form.field.VTypes, {
            emailText: '此欄位必須輸入像 "user@example.com" 之E-Mail格式',
            urlText: '此欄位必須輸入像 "http:/' + '/www.example.com" 之網址格式',
            alphaText: '此欄位僅能輸入半形英文字母及底線( _ )符號',
            alphanumText: '此欄位僅能輸入半形英文字母、數字及底線( _ )符號'
        });
    }
});

Ext.define("Ext.locale.zh_TW.view.View", {
    override: "Ext.view.View",
    emptyText: ""
});

Ext.define("Ext.locale.zh_TW.grid.plugin.DragDrop", {
    override: "Ext.grid.plugin.DragDrop",
    dragText: "選擇了 {0} 行"
});

Ext.define("Ext.locale.zh_TW.TabPanelItem", {
    override: "Ext.TabPanelItem",
    closeText: "關閉此標籤"
});

Ext.define("Ext.locale.zh_TW.form.field.Base", {
    override: "Ext.form.field.Base",
    invalidText: "數值不符合欄位規定"
});

// changing the msg text below will affect the LoadMask
Ext.define("Ext.locale.zh_TW.view.AbstractView", {
    override: "Ext.view.AbstractView",
    msg: "讀取中..."
});

Ext.define("Ext.locale.zh_TW.picker.Date", {
    override: "Ext.picker.Date",
    todayText: "今天",
    minText: "日期必須大於最小容許日期",
    maxText: "日期必須小於最大容許日期",
    disabledDaysText: "",
    disabledDatesText: "",
    monthNames: Ext.Date.monthNames,
    dayNames: Ext.Date.dayNames,
    nextText: "下個月 (Ctrl+右方向鍵)",
    prevText: "上個月 (Ctrl+左方向鍵)",
    monthYearText: "選擇月份 (Ctrl+上/下方向鍵選擇年份)",
    format: "y/m/d",
    ariaTitle: '{0}',
    ariaTitleDateFormat: 'Y\u5e74m\u6708d\u65e5',
    longDayFormat: 'Y\u5e74m\u6708d\u65e5',
    monthYearFormat: 'Y\u5e74m\u6708',
    getDayInitial: function(value) {
        // Grab the last character
        switch(value) {
            case "Sunday":
                 return "<span style='color:red'>日</span>";
                 break;
            case "Monday":
                return "一";
                break;
            case "Tuesday":
                return "二";
                break;
            case "Wednesday":
                return "三";
                break;
            case "Thursday":
                return "四";
                break;
            case "Friday":
                return "五";
                break;
            case "Saturday":
                return "<span style='color:red'>六</span>";
                break;
        }
        //return value.substr(value.length - 1);
    }
});

Ext.define("Ext.locale.zh_TW.picker.Month", {
    override: "Ext.picker.Month",
    okText: "确定",
    cancelText: "取消"
});

Ext.define("Ext.locale.zh_TW.toolbar.Paging", {
    override: "Ext.PagingToolbar",
    beforePageText: "第",
    afterPageText: "頁，共{0}頁",
    firstText: "第一頁",
    prevText: "上一頁",
    nextText: "下一頁",
    lastText: "最後頁",
    refreshText: "重新整理",
    displayMsg: "顯示{0} - {1}筆,共{2}筆",
    emptyMsg: '沒有任何資料'
});

Ext.define("Ext.locale.zh_TW.form.field.Text", {
    override: "Ext.form.field.Text",
    minLengthText: "此欄位最少要輸入 {0} 個字",
    maxLengthText: "此欄位最多輸入 {0} 個字",
    blankText: "此欄位為必填",
    regexText: "",
    emptyText: null
});

Ext.define("Ext.locale.zh_TW.form.field.Number", {
    override: "Ext.form.field.Number",
    minText: "此欄位之數值必須大於 {0}",
    maxText: "此欄位之數值必須小於 {0}",
    nanText: "{0} 不是合法的數字"
});

Ext.define("Ext.locale.zh_TW.form.field.Date", {
    override: "Ext.form.field.Date",
    disabledDaysText: "無法使用",
    disabledDatesText: "無法使用",
    minText: "此欄位之日期必須在 {0} 之後",
    maxText: "此欄位之日期必須在 {0} 之前",
    invalidText: "{0} 不是正確的日期格式 - 必須像是 「 {1} 」 這樣的格式",
    format: "Y/m/d"
});

Ext.define("Ext.locale.zh_TW.form.field.ComboBox", {
    override: "Ext.form.field.ComboBox",
    valueNotFoundText: undefined
}, function() {
    Ext.apply(Ext.form.field.ComboBox.prototype.defaultListConfig, {
        loadingText: "讀取中 ..."
    });
});

Ext.define("Ext.locale.zh_TW.grid.header.Container", {
    override: "Ext.grid.header.Container",
    sortAscText: "正向排序",
    sortDescText: "反向排序",
    lockText: "鎖定欄位",
    unlockText: "解開欄位鎖定",
    columnsText: "欄位"
});

Ext.define("Ext.locale.zh_TW.grid.PropertyColumnModel", {
    override: "Ext.grid.PropertyColumnModel",
    nameText: "名稱",
    valueText: "數值",
    dateFormat: "Y/m/d"
});

// This is needed until we can refactor all of the locales into individual files
Ext.define("Ext.locale.zh_TW.Component", {	
    override: "Ext.Component"
});

