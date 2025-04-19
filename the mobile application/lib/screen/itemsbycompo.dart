// ignore_for_file: library_private_types_in_public_api

import 'package:aqttan2/model/item.dart';
import 'package:aqttan2/screen/home.dart';
import 'package:aqttan2/widget/item_card.dart';
import 'package:flutter/material.dart';
import 'package:aqttan2/model/server.dart';
import '../widget/function.dart'; // يحتوي على البار والدارور

class ItemsC extends StatefulWidget {
  final int compoId; // استقبال compoId
  const ItemsC({required this.compoId, super.key});

  @override
  _ItemsCState createState() => _ItemsCState();
}

class _ItemsCState extends State<ItemsC> {
  Future<List<Item>>? futureItem = Future.value([]);

  @override
  void initState() {
    super.initState();
    _fetchItems();
  }

  Future<void> _fetchItems() async {
    setState(() {
      futureItem = ItemService.fetchItem(widget.compoId);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: barAQ(),
      drawer: darwerAQ(),
      body: SafeArea(
        child: Container(
          width: double.infinity,
          decoration: baseColors(),
          child: Column(
            children: [
              Expanded(
                child: RefreshIndicator(
                  color: const Color.fromARGB(255, 198, 102, 81),
                  onRefresh: _fetchItems, // تفعيل إعادة الجلب عند السحب لأسفل
                  child: FutureBuilder<List<Item>>(
                    future: futureItem,
                    builder: (context, snapshot) {
                      if (snapshot.hasData) {
                        return _buildItemList(snapshot.data!);
                      } else if (snapshot.hasError) {
                        return Center(
                            child: Text("حدث خطأ: ${snapshot.error}"));
                      }
                      return const Center(
                          child: CircularProgressIndicator(
                        valueColor: AlwaysStoppedAnimation<Color>(
                            Color.fromARGB(255, 50, 38, 38)),
                      ));
                    },
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
      floatingActionButton: FloatingActionButton(
          foregroundColor: const Color.fromARGB(255, 0, 0, 0),
          backgroundColor: const Color.fromARGB(255, 224, 118, 79),
          onPressed: () {
            Navigator.push(
              context,
              MaterialPageRoute(
                builder: (context) => const Home(),
              ),
            );
          },
          child: const Icon(Icons.home)),
      floatingActionButtonLocation: FloatingActionButtonLocation.endTop,
      floatingActionButtonAnimator: FloatingActionButtonAnimator.scaling,
    );
  }

  Widget _buildItemList(List<Item> items) {
    return GridView.builder(
      padding: const EdgeInsets.all(10),
      gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: 2,
        crossAxisSpacing: 10,
        mainAxisSpacing: 10,
        childAspectRatio: 0.7, // التحكم بنسبة عرض إلى ارتفاع الكارد
      ),
      itemCount: items.length,
      itemBuilder: (context, index) {
        return ItemCard(
          item: items[index],
          onTap: () {
            // التعامل مع الحدث عند الضغط
          },
        );
      },
    );
  }
}
