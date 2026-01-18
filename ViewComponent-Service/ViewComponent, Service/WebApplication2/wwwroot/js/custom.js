let loadMore = document.querySelector('.load-more');

loadMore.addEventListener('click', function () {

    let htmlProductCount = document.querySelectorAll(".load-products .product").length;
    let dbProductCount = document.querySelector(".load-products .product-count").value;
    //console.log(dbProductCount);
    //console.log(productCount);
    fetch(`Home/LoadMore?skip=${htmlProductCount}`)
        .then(response => response.text())
        .then(response => {
            let parent = document.querySelector(".load-products");
            parent.innerHTML += response;

          htmlProductCount = document.querySelectorAll(".load-products .product").length;

            if (htmlProductCount == dbProductCount) {
                this.style.display = 'none';
            }
        });
});

let addBtn = document.querySelectorAll(".btn-primary");

addBtn.forEach(btn => {
    btn.addEventListener("click", function (e) {
        e.preventDefault();

        let id = this.getAttribute("product-id");

        fetch(`/Basket/Add?id=${id}`, {
            method: "POST"
        })
            .then(res => {
                if (res.ok) {
                    console.log("Product added to basket");
                }
            });
    });
});
