import 'package:aqttan2/model/compo.dart';
import 'dart:convert';
import 'dart:typed_data';

class Item {
  final int id;
  final String name;
  final String description;
  final double price;
  final Uint8List image;
  final Compo? compo;
  final int compoId;

  Item({
    required this.id,
    required this.name,
    required this.description,
    required this.price,
    required this.image,
    this.compo,
    required this.compoId,
  });

  factory Item.fromJson(Map<String, dynamic> json) {
    return Item(
      id: json['id'],
      name: json['name'],
      description: json['description'],
      price: json['price'],
      image: base64Decode(json['image']),
      compo: json['compo'],
      compoId: json['compoId'],
    );
  }
}
