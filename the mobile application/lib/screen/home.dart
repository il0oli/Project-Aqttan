// ignore_for_file: library_private_types_in_public_api

import 'package:aqttan2/screen/itemsbycompo.dart';
import 'package:aqttan2/widget/compo_card.dart';
import 'package:flutter/material.dart';
import 'package:aqttan2/model/server.dart';
import '../model/compo.dart';
import '../widget/function.dart'; // يحتوي على البار والدارور

class Home extends StatefulWidget {
  const Home({super.key});

  @override
  _HomeState createState() => _HomeState();
}

class _HomeState extends State<Home> {
  Future<List<Compo>>? futureCompo = Future.value([]);

  @override
  void initState() {
    super.initState();
    _fetchCompos();
  }

  Future<void> _fetchCompos() async {
    setState(() {
      futureCompo = CompoService.fetchCompo();
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
                  onRefresh: _fetchCompos, // تفعيل إعادة الجلب عند السحب لأسفل
                  child: FutureBuilder<List<Compo>>(
                    future: futureCompo,
                    builder: (context, snapshot) {
                      if (snapshot.hasData) {
                        return _buildCompoList(snapshot.data!);
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
    );
  }

  Widget _buildCompoList(List<Compo> compos) {
    return GridView.builder(
      padding: const EdgeInsets.all(10),
      gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: 2,
        crossAxisSpacing: 10,
        mainAxisSpacing: 10,
        childAspectRatio: 0.8, // التحكم بنسبة عرض إلى ارتفاع الكارد
      ),
      itemCount: compos.length,
      itemBuilder: (context, index) {
        return CompoCard(
          compo: compos[index],
          onTap: () {
            Navigator.push(
              context,
              MaterialPageRoute(
                builder: (context) => ItemsC(compoId: compos[index].id),
              ),
            );
          },
        );
      },
    );
  }
}
