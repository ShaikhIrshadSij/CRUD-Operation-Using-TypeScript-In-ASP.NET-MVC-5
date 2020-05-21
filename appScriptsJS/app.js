var $;
var Test = /** @class */ (function () {
    function Test() {
    }
    Test.prototype.testClass = function () {
        fetch('https://api.myjson.com/bins/zg8of').then(function (res) { return res.json(); }).then(function (res) {
            console.log(res);
        });
        return this.testObj;
    };
    return Test;
}());
var isNullOrUndefined = function (obj) {
    return typeof obj === "undefined" || obj === null;
};
//# sourceMappingURL=app.js.map