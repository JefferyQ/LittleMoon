//teacherlist.js
//获取应用实例
var app = getApp()
Page({
  data: {
    adrimg: '../../image/icon-address.png',
    phoneimg: '../../image/icon-phone.png',
    shareimg:'../../image/icon-share.png',
    imgUrls: [
      'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1495626232647&di=c7c53f96e0f48681471e4626eebe0181&imgtype=0&src=http%3A%2F%2Fwww.sanchiseo.com%2Fuploadfile%2F2015821%2F2015821115728937045.jpg',
      'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1495626316444&di=ff20f74da6031541a12e0eeadaf156b9&imgtype=0&src=http%3A%2F%2Fsem.g3img.com%2Fsite%2F34016275%2F20160216162430_82108.png',
      'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1495626359075&di=3297d75c3742024d15f553547495f3db&imgtype=0&src=http%3A%2F%2Fwww.17emarketing.com%2Fuploads%2Fallimg%2F160627%2F1-16062G54154.png',
    ],
    trimg: 'http://img.xdf.cn/TeacherIMG/2011/20110412/TCCQ09120110412170811_big.jpg',
    indicatorDots: true,
    autoplay: true,
    interval: 5000,
    duration: 1000
  },
  getLocation: function () {
    wx.openLocation({
      latitude: 23.578226,
      longitude: 110.111042,
      name: "黄姐理疗中心",
      address: "广西桂平市金田加油站对面黄姐理疗中心",
      scale: 28
    })
  },
})