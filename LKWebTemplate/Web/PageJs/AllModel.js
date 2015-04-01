/*BASIC*/
Ext.define('COMPANY', {
    extend: 'Ext.data.Model',
    fields: ['UUID', 'ID', 'C_NAME', 'E_NAME', 'WEEK_SHIFT', 'NAME_ZH_CN', 'IS_ACTIVE']
});

Ext.define('APPLICATION', {
    extend: 'Ext.data.Model',
    fields: [
        'CREATE_DATE',
        'UPDATE_DATE',
        'IS_ACTIVE',
        'NAME',
        'DESCRIPTION',
        'ID',
        'CREATE_USER',
        'UPDATE_USER',
        'WEB_SITE',
        'UUID'
    ]
});

Ext.define('APPLICATIONHEADV', {
    extend: 'Ext.data.Model',
    fields: [
        'CREATE_DATE',
        'CREATE_USER',
        'DESCRIPTION',
        'ID',
        'IS_ACTIVE',
        'NAME',
        'UPDATE_DATE',
        'UPDATE_USER',
        'UUID',
        'WEB_SITE'
    ]
});

Ext.define('APPPAGE', {
    extend: 'Ext.data.Model',
    fields: [
        'UUID',
        'IS_ACTIVE',
        'CREATE_DATE',
        'CREATE_USER',
        'UPDATE_DATE',
        'UPDATE_USER',
        'ID',
        'NAME',
        'DESCRIPTION',
        'URL',
        'PARAMETER_CLASS',
        'APPLICATION_HEAD_UUID',
        'P_MODE',
        'RUNJSFUNCTION'
    ]
});

Ext.define('ATTEDNANTVV', {
    extend: 'Ext.data.Model',
    fields: [
        'COMPANY_ID',
        'COMPANY_C_NAME',
        'COMPANY_E_NAME',
        'DEPARTMENT_ID',
        'DEPARTMENT_C_NAME',
        'DEPARTMENT_E_NAME',
        'SITE_ID',
        'SITE_C_NAME',
        'SITE_E_NAME',
        'UUID',
        'CREATE_DATE',
        'UPDATE_DATE',
        'IS_ACTIVE',
        'COMPANY_UUID',
        'ACCOUNT',
        'C_NAME',
        'E_NAME',
        'EMAIL',
        'PASSWORD',
        'IS_SUPPER',
        'IS_ADMIN',
        'CODE_PAGE',
        'DEPARTMENT_UUID',
        'PHONE',
        'SITE_UUID',
        'GENDER',
        'BIRTHDAY',
        'HIRE_DATE',
        'QUIT_DATE',
        'IS_MANAGER',
        'IS_DIRECT',
        'GRADE',
        'ID',
        'IS_DEFAULT_PASS'
    ]
});


Ext.define('DEPARTMENT', {
    extend: 'Ext.data.Model',
    fields: [
        'UUID', 'CREATE_DATE', 'UPDATE_DATE', 'IS_ACTIVE',
        'COMPANY_UUID', 'ID', 'C_NAME',
        'E_NAME', 'PARENT_DEPARTMENT_UUID',
        'MANAGER_UUID', 'PARENT_DEPARTMENT_ID', 'MANAGER_ID', 'PARENT_DEPARTMENT_UUID_LIST',
        'S_NAME', 'COST_CENTER', 'SRC_UUID', 'FULL_DEPARTMENT_NAME'
    ]
});

Ext.define('ERROR_LOG', {
    extend: 'Ext.data.Model',
    fields: ['UUID', 'ERROR_CODE', 'ERROR_TIME',
        'ERROR_MESSAGE', 'APPLICATION_NAME', 'ATTENDANT_UUID',
        'ERROR_TYPE', 'IS_READ', 'CREATE_DATE', 'C_NAME'
    ]
});

Ext.define('MENU', {
    extend: 'Ext.data.Model',
    fields: ['UUID', 'IS_ACTIVE', 'CREATE_DATE', 'CREATE_USER', 'UPDATE_DATE', 'UPDATE_USER', 'NAME_ZH_TW', 'NAME_ZH_CN', 'NAME_EN_US', 'ID', 'APPMENU_UUID', 'HASCHILD', 'APPLICATION_HEAD_UUID', 'ORD', 'PARAMETER_CLASS', 'IMAGE', 'SITEMAP_UUID', 'ACTION_MODE', 'IS_DEFAULT_PAGE', 'IS_ADMIN']
});

Ext.define('SITEMAP', {
    extend: 'Ext.data.Model',
    fields: ['UUID', 'IS_ACTIVE', 'CREATE_DATE', 'CREATE_USER', 'UPDATE_DATE', 'UPDATE_USER', 'SITEMAP_UUID', 'APPPAGE_UUID', 'ROOT_UUID', 'HASCHILD', 'APPLICATION_HEAD_UUID', 'NAME', 'DESCRIPTION', 'URL', 'P_MODE', 'PARAMETER_CLASS', 'APPPAGE_IS_ACTIVE']
});

Ext.define('PROXY', {
    extend: 'Ext.data.Model',
    fields: [
        'UUID',
        'PROXY_ACTION',
        'PROXY_METHOD',
        'DESCRIPTION',
        'PROXY_TYPE',
        'NEED_REDIRECT',
        'REDIRECT_PROXY_ACTION',
        'REDIRECT_PROXY_METHOD',
        'APPLICATION_HEAD_UUID',
        'REDIRECT_SRC'
    ]
});

Ext.define('GROUP_HEAD_V', {
    extend: 'Ext.data.Model',
    fields: [
        'UUID',
        'CREATE_DATE',
        'UPDATE_DATE',
        'IS_ACTIVE',
        'NAME_ZH_TW',
        'NAME_ZH_CN',
        'NAME_EN_US',
        'COMPANY_UUID',
        'ID',
        'APPLICATION_HEAD_UUID',
        'APPLICATION_HEAD_ID',
        'APPLICATION_HEAD_NAME'
    ]
});

Ext.define('V_APPMENU_PROXY_MAP', {
    extend: 'Ext.data.Model',
    fields: [
        'PROXY_UUID',
        'PROXY_ACTION',
        'PROXY_METHOD',
        'PROXY_DESCRIPTION',
        'PROXY_TYPE',
        'NEED_REDIRECT',
        'REDIRECT_PROXY_ACTION',
        'REDIRECT_PROXY_METHOD',
        'REDIRECT_SRC',
        'APPLICATION_HEAD_UUID',
        'NAME_ZH_TW',
        'NAME_ZH_CN',
        'NAME_EN_US',
        'UUID',
        'APPMENU_PROXY_UUID',
        'APPMENU_UUID',
    ]
});

Ext.define('SCHEDULE', {
    extend: 'Ext.data.Model',
    fields: [
        'UUID',
        'SCHEDULE_NAME',
        'SCHEDULE_END_DATE',
        'LAST_RUN_TIME',
        'LAST_RUN_STATUS',
        'IS_CYCLE',
        'SINGLE_DATE',
        'HOUR',
        'MINUTE',
        'CYCLE_TYPE',
        'C_MINUTE',
        'C_HOUR',
        'C_DAY',
        'C_WEEK',
        'C_DAY_OF_WEEK',
        'C_MONTH',
        'C_DAY_OF_MONTH',
        'C_WEEK_OF_MONTH',
        'C_YEAR',
        'C_WEEK_OF_YEAR',
        'RUN_URL',
        'RUN_URL_PARAMETER',
        'RUN_ATTENDANT_UUID',
        'IS_ACTIVE',
        'START_DATE',
        'RUN_SECURITY'
    ]
});
