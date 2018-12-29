//index.js
//获取应用实例
var app = getApp()
Page({
  data: {
    a1src:'../../image/a1.png',
    a2src: '../../image/a2.png',
    a3src: '../../image/a3.png',
    a4src: '../../image/a4.png',
    adrimg: '../../image/icon-address.png',
    signupimg:'../../image/signup.png',
    jtimg: '../../image/icon-jt.png',
    phoneimg: '../../image/icon-phone.png',
    currentData: 0,
    imgsrc:'https://ss0.bdstatic.com/9bA1vGfa2gU2pMbfm9GUKT-w/timg?searchbox_feed&quality=120&wh_rate=0&size=f648_364&ref=http%3A%2F%2Fwww.baidu.com&sec=0&di=1619440b1bc5580ece1e18c556a408d7&src=http%3A%2F%2Ft12.baidu.com%2Fit%2Fu%3D2318927072%2C2551034291%26fm%3D175%26s%3DF9E09B544C2154095A6098DF030090F1%26w%3D660%26h%3D370%26img.JPEG',
    iconsrc:'../../image/usercount.png',
    jtsrc:'../../image/icon-jt.png',
    imgUrls: [
      'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1495626232647&di=c7c53f96e0f48681471e4626eebe0181&imgtype=0&src=http%3A%2F%2Fwww.sanchiseo.com%2Fuploadfile%2F2015821%2F2015821115728937045.jpg',
      'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1495626316444&di=ff20f74da6031541a12e0eeadaf156b9&imgtype=0&src=http%3A%2F%2Fsem.g3img.com%2Fsite%2F34016275%2F20160216162430_82108.png',
      'https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1495626359075&di=3297d75c3742024d15f553547495f3db&imgtype=0&src=http%3A%2F%2Fwww.17emarketing.com%2Fuploads%2Fallimg%2F160627%2F1-16062G54154.png',
    ],
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
  onShareAppMessage: function (res) {
    if (res.from === 'button') {
      // 来自页面内转发按钮
      console.log(res.target)
    }
    return {
      title: '黄姐理疗中心首页',
      path: '/pages/index/index',
      success: function (res) {
        // 转发成功
        console.log("转发成功")
      },
      fail: function (res) {
        // 转发失败
        onsole.log("转发失败")
      }
    }
  },
  //获取当前滑块的index
  bindchange: function (e) {
    const that = this;
    that.setData({
      currentData: e.detail.current
    })
  },
  //点击切换，滑块index赋值
  checkCurrent: function (e) {
    const that = this;

    if (that.data.currentData === e.target.dataset.current) {
      return false;
    } else {

      that.setData({
        currentData: e.target.dataset.current
      })
    }
  },
  testRequest:function(){
    wx.request({
      url: 'http://localhost:8089/api/values/getvalue?value=4', // 仅为示例，并非真实的接口地址
      header: {
        'content-type': 'application/json' // 默认值
      },
      success(res) {
        console.log(res)
      }
    })
  }
})
