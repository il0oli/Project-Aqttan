import 'dart:convert';
import 'package:aqttan2/model/compo.dart';
import 'package:aqttan2/model/item.dart';
import 'package:http/http.dart' as http;

class CompoService {
  static Future<List<Compo>> fetchCompo() async {
    final response =
        await http.get(Uri.parse('https://192.168.0.114:45455/api/Compos'));

    if (response.statusCode == 200) {
      List jsonResponse = json.decode(response.body);
      return jsonResponse.map((data) => Compo.fromJson(data)).toList();
    } else {
      throw Exception('فشل في تحميل البيانات');
    }
  }
}

class ItemService {
  static Future<List<Item>> fetchItem(int componentId) async {
    final response = await http.get(Uri.parse(
        'https://192.168.0.114:45455/api/Items/byComponent/$componentId'));

    if (response.statusCode == 200) {
      List jsonResponse = json.decode(response.body);
      return jsonResponse.map((data) => Item.fromJson(data)).toList();
    } else {
      throw Exception('فشل في تحميل البيانات');
    }
  }
}
