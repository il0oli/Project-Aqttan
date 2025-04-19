import 'package:flutter/material.dart';

//For Background Colors
BoxDecoration baseColors() {
  return const BoxDecoration(
    gradient: LinearGradient(
      begin: Alignment.topCenter,
      end: Alignment.bottomCenter,
      colors: [
        Color.fromARGB(255, 255, 255, 255),
        Color.fromARGB(255, 255, 255, 255),
      ],
    ),
  );
}

AppBar barAQ() {
  return AppBar(
    title: const Padding(
      padding: EdgeInsets.only(left: 100),
      child: Text(
        'أقطان',
        style: TextStyle(
          fontFamily: 'SF',
          fontSize: 30.0,
        ),
      ),
    ),
    backgroundColor: const Color.fromARGB(255, 231, 132, 110),
  );
}

Drawer darwerAQ() {
  return Drawer(
      child: ListView(
    padding: EdgeInsets.zero,
    children: [
      DrawerHeader(
        decoration: baseColors(),
        child: const Center(
          child: Text(
            'اقطان يسهل عليك تصفح منتجات المصممه زنوتشا وطلب اي منتج عبر الواتساب',
            style: TextStyle(fontFamily: 'SF', fontSize: 17.0),
            textAlign: TextAlign.center,
          ),
        ),
      ),
      ListTile(
        title: const Text(
          'Item 1',
          style: TextStyle(
            fontFamily: 'SF',
          ),
        ),
        onTap: () {
          // Handle item 1 tap.
        },
      ),
      ListTile(
        title: const Text(
          'Item 2',
          style: TextStyle(
            fontFamily: 'SF',
          ),
        ),
        onTap: () {
          // Handle item 2 tap.
        },
      )
    ],
  ));
}
