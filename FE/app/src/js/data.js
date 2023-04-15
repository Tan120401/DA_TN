const PRODUCTS = [
  {
    id: 1,
    picture: "1.1nike.png",
    picture2: "1.2nike.png",
    picture3: "1.3nike.png",
    picture4: "1.4nike.png",
    productname: "Nike",
    price: 1000000,
    pricediscount: 900000,
  },
  {
    id: 2,
    picture: "2.1nike.png",
    picture2: "2.2nike.png",
    picture3: "2.3nike.png",
    picture4: "2.4nike.png",
    productname: "Nike",
    price: 1000000,
    pricediscount: 800000,
  },
  {
    id: 3,
    picture: "3.1nike.png",
    productname: "Nike",
    price: 1000000,
    pricediscount: 900000,
  },
  {
    id: 4,
    picture: "4.1nike.png",
    productname: "Nike",
    price: 1000000,
    pricediscount: 900000,
  },
  {
    id: 5,
    picture: "5.1nike.png",
    productname: "Nike",
    price: 1000000,
    pricediscount: 900000,
  },
  {
    id: 6,
    picture: "6.1nike.png",
    productname: "Nike",
    price: 1000000,
    pricediscount: 900000,
  },
  {
    id: 7,
    picture: "7.1nike.png",
    productname: "Nike",
    price: 1000000,
    pricediscount: 900000,
  },
  {
    id: 8,
    picture: "9.1nike.png",
    productname: "Nike",
    price: 1000000,
    pricediscount: 900000,
  },
];
const PRODUCTS_CART = [
  {
    id: 1,
    picture: "1.1nike.png",
    productname: "Nike",
    numproduct: 1,
    size: 39,
    price: 1000000,
    pricediscount: 900000,
  },
  {
    id: 2,
    picture: "2.1nike.png",
    productname: "Nike2",
    size: 40,
    numproduct: 1,
    price: 1000000,
    pricediscount: 800000,
  },
];
const NEWS = [
  {
    newpic:
      "https://file.hstatic.net/1000282067/file/new-balance-550-release-date_8142186bc207493da37ac410f95bbb11_1024x1024.jpg",
    newcontent:
      "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Veritatis tenetur, iusto hic at distinctio perspiciatis error commodi, obcaecati debitis possimus consectetur temporibus est optio aut voluptatem, repudiandae expedita asperiores suscipit.",
  },
  {
    newpic:
      "https://file.hstatic.net/1000282067/article/www.highsnobiety__42__26b858ec8fae436aa8031e79948bf05b_large.png",
    newcontent:
      "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Veritatis tenetur, iusto hic at distinctio perspiciatis error commodi, obcaecati debitis possimus consectetur temporibus est optio aut voluptatem, repudiandae expedita asperiores suscipit.",
  },
  {
    newpic:
      "http://file.hstatic.net/1000282067/file/_off-white-nike-dunk-low-the-50-black-silver-dm1602-001-release-info-2_d2452dc3498c4b7f8f7b134700d8c976_2048x2048.jpg",
    newcontent:
      "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Veritatis tenetur, iusto hic at distinctio perspiciatis error commodi, obcaecati debitis possimus consectetur temporibus est optio aut voluptatem, repudiandae expedita asperiores suscipit.",
  },
  {
    newpic:
      "https://file.hstatic.net/1000282067/article/www.highsnobiety__38__c6388e678d784f40ac3cd421e97ce6a2_large.png",
    newcontent:
      "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Veritatis tenetur, iusto hic at distinctio perspiciatis error commodi, obcaecati debitis possimus consectetur temporibus est optio aut voluptatem, repudiandae expedita asperiores suscipit.",
  },
];

const USER = {
  username: "",
  password: "",
  fullname: "",
  email: "",
  role: null,
};

const USER_REGISTER = {
  Email: "",
  FullName: "",
  Role: "user",
  UserName: "",
  Password: "",
};

const FILE_OPTIONS = {
  FileName: null,
  File: null,
}

export { PRODUCTS, NEWS, PRODUCTS_CART, USER, USER_REGISTER,FILE_OPTIONS };
